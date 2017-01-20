using System;
using System.Collections.Generic;
using CocosSharp;

namespace PalitoBall
{
    public class GameLayer : CCLayerColor
    {
        CCSprite paddleSprite;
        CCSprite ballSprite;
        CCLabel scoreLabel;

        float ballXVelocity;
        float ballYVelocity;
        // gravidade da bola. em segundos
        const float gravity = 140;
        int score;

        public GameLayer() : base(CCColor4B.Black)
        {

            paddleSprite = new CCSprite("paddle");//Nossa Imagem
            paddleSprite.PositionX = 100;
            paddleSprite.PositionY = 100;
            AddChild(paddleSprite);

            ballSprite = new CCSprite("ball");//nossa imagem
            ballSprite.PositionX = 320;
            ballSprite.PositionY = 600;
            AddChild(ballSprite);

            scoreLabel = new CCLabel("Score: 0", "Arial", 20, CCLabelFormat.SystemFont);
            scoreLabel.PositionX = 50;
            scoreLabel.PositionY = 1000;
            scoreLabel.AnchorPoint = CCPoint.AnchorUpperLeft;
            AddChild(scoreLabel);

            Schedule(RunGameLogic);

        }

        void RunGameLogic(float frameTimeInSeconds)
        {
            // Vamos gerar um movimento linear com base na gravidade definida.
            ballYVelocity += frameTimeInSeconds * -gravity;
            ballSprite.PositionX += ballXVelocity * frameTimeInSeconds;
            ballSprite.PositionY += ballYVelocity * frameTimeInSeconds;           
            // verifique se os dois CCSprintes se sobrepoem...
            bool doesBallOverlapPaddle = ballSprite.BoundingBoxTransformedToParent.IntersectsRect(
                paddleSprite.BoundingBoxTransformedToParent);
            // ... e se a bola esta indo para baixo
            bool isMovingDownward = ballYVelocity < 0;
            if (doesBallOverlapPaddle && isMovingDownward)
            {
                // Vamos inverter a velocidade
                ballYVelocity *= -1;
                // vamos atribuir um valor aleatorio a velocidade X da bola
                const float minXVelocity = -300;
                const float maxXVelocity = 300;
                ballXVelocity = CCRandom.GetRandomFloat(minXVelocity, maxXVelocity);
                
                score++;
                scoreLabel.Text = "Score: " + score;
            }
            // Capturamos a posicao da bola:   
            float ballRight = ballSprite.BoundingBoxTransformedToParent.MaxX;
            float ballLeft = ballSprite.BoundingBoxTransformedToParent.MinX;
            // capturamos as laterias da tela
            float screenRight = VisibleBoundsWorldspace.MaxX;
            float screenLeft = VisibleBoundsWorldspace.MinX;

            // Verifique se a bola esta muito longe para a direita ou para a esquerda   
            bool shouldReflectXVelocity =
                (ballRight > screenRight && ballXVelocity > 0) ||
                (ballLeft < screenLeft && ballXVelocity < 0);

            if (shouldReflectXVelocity)
            {
                ballXVelocity *= -1;
            }
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Use os limites para mapear o posicionamento
            CCRect bounds = VisibleBoundsWorldspace;

            // Registra os eventos de toque na tela
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            touchListener.OnTouchesMoved = HandleTouchesMoved;
            AddEventListener(touchListener, this);
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                // Perform touch handling here
            }
        }

        void HandleTouchesMoved(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        {
            // devemos nos preocupar apenas com o primeiro toque na tela
            var locationOnScreen = touches[0].Location;
            paddleSprite.PositionX = locationOnScreen.X;
        }
    }
}

