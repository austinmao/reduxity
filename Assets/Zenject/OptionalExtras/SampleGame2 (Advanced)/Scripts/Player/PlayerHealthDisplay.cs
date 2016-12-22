using ModestTree;
using UnityEngine;
using Zenject;
using System;

#pragma warning disable 649

namespace Zenject.SpaceFighter
{
    public class PlayerHealthDisplay : MonoBehaviour, IDisposable, IInitializable
    {
        [SerializeField]
        float _leftPadding;

        [SerializeField]
        float _bottomPadding;

        [SerializeField]
        float _labelWidth;

        [SerializeField]
        float _labelHeight;

        [SerializeField]
        float _textureWidth;

        [SerializeField]
        float _textureHeight;

        [SerializeField]
        float _killCountOffset;

        [SerializeField]
        Color _foregroundColor;

        [SerializeField]
        Color _backgroundColor;

        PlayerModel _model;
        Texture2D _textureForeground;
        Texture2D _textureBackground;
        int _killCount;

        EnemyKilledSignal _enemyKilledSignal;

        [Inject]
        public void Construct(PlayerModel model, EnemyKilledSignal enemyKilledSignal)
        {
            _model = model;
            _enemyKilledSignal = enemyKilledSignal;
        }

        public void Initialize()
        {
            _textureForeground = CreateColorTexture(_foregroundColor);
            _textureBackground = CreateColorTexture(_backgroundColor);

            _enemyKilledSignal += OnEnemyKilled;
        }

        public void Dispose()
        {
            _enemyKilledSignal -= OnEnemyKilled;
        }

        void OnEnemyKilled()
        {
            _killCount++;
        }

        Texture2D CreateColorTexture(Color color)
        {
            var texture = new Texture2D(1, 1);
            texture.SetPixel(1, 1, color);
            texture.Apply();
            return texture;
        }

        public void OnGUI()
        {
            var healthLabelBounds = new Rect(_leftPadding, Screen.height - _bottomPadding, _labelWidth, _labelHeight);
            GUI.Label(healthLabelBounds, "Health: {0:0}".Fmt(_model.Health));

            var killLabelBounds = new Rect(healthLabelBounds.xMin, healthLabelBounds.yMin - _killCountOffset, _labelWidth, _labelHeight);
            GUI.Label(killLabelBounds, "Kill Count: {0}".Fmt(_killCount));

            var boundsBackground = new Rect(healthLabelBounds.xMax, healthLabelBounds.yMin, _textureWidth, _textureHeight);
            GUI.DrawTexture(boundsBackground, _textureBackground);

            var boundsForeground = new Rect(boundsBackground.xMin, boundsBackground.yMin, (_model.Health / 100.0f) * _textureWidth, _textureHeight);
            GUI.DrawTexture(boundsForeground, _textureForeground);
        }
    }
}
