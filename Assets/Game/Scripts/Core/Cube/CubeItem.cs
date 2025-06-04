using R3;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Cube
{
    public class CubeItem : MonoBehaviour, ISavedPlayerProgress
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CubeAnimator _animator;
        [SerializeField] private Image _image;

        private int _itemID;
        private CubeState _cubeState;
        private int _spriteId;

        public Subject<Unit> OnItemPlaced = new();

        public Vector2 Position => RectTransform.position;
        public float Width => RectTransform.rect.width * RectTransform.localScale.x;
        public float Height => RectTransform.rect.height * RectTransform.localScale.y;

        public RectTransform RectTransform => _rectTransform;

        public int ItemID => _itemID;

        public void AnimateMiss(Action callback)
        {
            _animator.AnimateMiss(() => callback?.Invoke());
        }

        public void AnimatePlaceInTower(Vector3 targetPos)
        {
            _animator.AnimatePlaceWithHorizontalOffset(targetPos, () =>
            {
                OnItemPlaced.OnNext(Unit.Default);
            });
        }

        public void AnimateFallDown()
        {
            _animator.AnimateFallDown(Height);
        }

        public void AnimateDelete(Action callback)
        {
            AnimateMiss(callback);
        }

        public void AnimatePlace(Vector2 position)
        {
            _animator.AnimatePlace(position);
        }

        public void SetSprite(Sprite sprite, int spriteId)
        {
            _image.sprite = sprite;
            _spriteId = spriteId;
        }

        public void SetID(int id)
        {
            _itemID = id;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (progress.WorldData.Items.Any((item) => item.ID == _itemID))
            {
                ItemData itemData = progress.WorldData.Items.First((item) => item.ID == _itemID);
                itemData.ID = _itemID;
                itemData.State = _cubeState;
                itemData.PositionOnLevel.Vector3Data = _rectTransform.anchoredPosition.AsVectorData();
                itemData.SpriteId = _spriteId;
            }
            else
            {
                var itemData = new ItemData();
                itemData.ID = _itemID;
                itemData.State = _cubeState;
                itemData.PositionOnLevel.Vector3Data = _rectTransform.anchoredPosition.AsVectorData();
                itemData.SpriteId = _spriteId;
                progress.WorldData.Items.Add(itemData);
            }
        }

        public void ChangeState(CubeState state)
        {
            _cubeState = state;
        }
    }
}