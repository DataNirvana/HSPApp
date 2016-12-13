using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------
namespace HSPApp.Layout {
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class SwipeableContentView : ContentView {

        public Boolean IsPanUpdate = false;
        double inicioX, fimX, inicioY, fimY;

        public event EventHandler GoRight; // Right
        public event EventHandler GoLeft; // Left
        public event EventHandler GoUp; // Top
        public event EventHandler GoDown; // Down


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------
        public SwipeableContentView() {

            PanGestureRecognizer pgRecogniser = new PanGestureRecognizer();
            pgRecogniser.PanUpdated += (s, e) => {
                // handle the tap
                OnPanUpdated(s, e);
            };
            this.GestureRecognizers.Add(pgRecogniser);


            Content = new Label { Text = "Hello View" };
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void OnPanUpdated(object sender, PanUpdatedEventArgs e) {
            this.IsPanUpdate = true;
            if (e.StatusType == GestureStatus.Started)
                inicioX = fimX = inicioY = fimY = 0;
            else if (e.StatusType == GestureStatus.Running) {
                if (inicioX == 0)
                    inicioX = e.TotalX;
                else
                    fimX = e.TotalX;

                if (inicioY == 0)
                    inicioY = e.TotalY;
                else
                    fimY = e.TotalY;
            } else if (e.StatusType == GestureStatus.Completed) {
                if (inicioX == inicioY) {
                    this.IsPanUpdate = false;
                } else if (inicioX < fimX) // Direita
                  {
                    if (GoRight != null)
                        GoRight(null, new EventArgs());
                } else if (inicioX > fimX) // Esquerda
                  {
                    if (GoLeft != null)
                        GoLeft(null, new EventArgs());
                }

                if (inicioY < fimY) // Baixo
                {
                    if (GoDown != null)
                        GoDown(null, new EventArgs());
                } else if (inicioY > fimY) // Cima
                  {
                    if (GoUp != null)
                        GoUp(null, new EventArgs());
                }

                inicioX = fimX = inicioY = fimY = 0;
            }
        }



    }
}
