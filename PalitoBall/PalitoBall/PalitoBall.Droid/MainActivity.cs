using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using CocosSharp;

using Microsoft.Azure.Mobile;


namespace PalitoBall.Droid
{
	[Activity (Label = "PalitoBall.Droid", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : Activity
    {
		protected override void OnCreate (Bundle bundle)
		{
            MobileCenter.Configure("5c6beb54-0606-4041-8ef4-44389f651a53");
            base.OnCreate(bundle);

            

            // Define o Layout como nossa visão principal do game
            SetContentView(Resource.Layout.Main);

            // Anexa ao GameView o metodo LoadGame
            CCGameView gameView = (CCGameView)FindViewById(Resource.Id.GameView);
            gameView.ViewCreated += LoadGame;
        }

        void LoadGame(object sender, EventArgs e)
        {
            //Criar o GameView
            CCGameView gameView = sender as CCGameView;
            if (gameView != null)
            {
                // carrega fonts e souds. Nosso app não usa, mas poderiamos usar sem problemas
                var contentSearchPaths = new List<string>() { "Fonts", "Sounds" };
                //Vamos definir nossa resolução padrão
                CCSizeI viewSize = gameView.ViewSize;
                int width = 768;
                int height = 1027;              
                gameView.DesignResolution = new CCSizeI(width, height);

                //Define a utilização de qualidade das imagens, voce pode ter mais opções
                // Verifique as resolucoes padroes dos sistemas que pretende disponibilizar seu game
                if (width < viewSize.Width)
                {
                    contentSearchPaths.Add("Images/Hd");
                    CCSprite.DefaultTexelToContentSizeRatio = 2.0f;
                }
                else
                {
                    contentSearchPaths.Add("Images/Ld");
                    CCSprite.DefaultTexelToContentSizeRatio = 1.0f;
                }

                // carrega para o Game os contens carregados
                gameView.ContentManager.SearchPaths = contentSearchPaths;
                // Passa a view do game para a cena principal do game
                CCScene gameScene = new CCScene(gameView);
                // adiona o Layer a cena
                // Lembra do CCScene> CCLayer> CCSprite  que falamos no inicio.
                gameScene.AddLayer(new GameLayer());
                gameView.RunWithScene(gameScene);
            }

        }
    }
}

