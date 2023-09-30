using UnityEngine;
using UnityEngine.UI;

namespace RustedGames
{
    public sealed class UIFollowBarEffect : MonoBehaviour
    {

        [SerializeField]
        private Image _ImageToFollow;

        [SerializeField]
        private RectTransform _FollowerRecTransform;

        [SerializeField]
        private RectTransform _ImageRecTransform;

        private float _EdgeMargin = 0f;

        private void Awake()
        {
            _EdgeMargin = _ImageRecTransform.rect.width / 2;
        }

        private void Update()
        {
            if (_ImageToFollow.type != Image.Type.Filled) return;

            _FollowerRecTransform.localPosition =
                new Vector2(_ImageToFollow.fillAmount * _ImageRecTransform.rect.width - _EdgeMargin,
                _FollowerRecTransform.localPosition.y);

        }

    }
}
