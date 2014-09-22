// Copyright (c) 2014 shobomaru
// This software is released under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;

// 配置
// http://msdn.microsoft.com/ja-jp/library/bb772100.aspx

namespace LeapSlides
{
    public partial class ThisAddIn
    {
        private PowerPoint.SlideShowWindow _slideWindow;

        private Leap.Controller _leapCtrl;
        private LeapListener _leap;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            try
            {
                _leapCtrl = new Leap.Controller();
                _leap = new LeapListener();
                _leap.GestureOccur += new EventHandler(gesture_Occur);

                Console.WriteLine("AddIn start.");
                MessageFilter.Register();

                this.Application.SlideShowBegin += StartPresentationEvent;
                this.Application.SlideShowEnd += StopPresentationEvent;
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddIn faild to start : " + ex.Message);
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            Console.WriteLine("AddIn stop.");
            MessageFilter.Revoke();

            if (_leapCtrl != null)
            {
                if (_leap != null)
                {
                    _leapCtrl.RemoveListener(_leap);
                    _leap.Dispose();
                    _leap = null;
                }
                _leapCtrl.Dispose();
                _leapCtrl = null;
            }
        }

        private void StartPresentationEvent(PowerPoint.SlideShowWindow Wn)
        {
            Console.Write("Presentation start.");
            _slideWindow = Wn;

            if (Config._value.IsEnabled)
            {
                if (_leapCtrl.IsConnected)
                {
                    _leapCtrl.AddListener(_leap);
                }
            }
            else
            {
                Console.WriteLine("AddIn is disabled so do nothing.");
            }
        }

        private void StopPresentationEvent(PowerPoint.Presentation Wn)
        {
            Console.WriteLine("Presentation end.");
            _slideWindow = null;
            if (_leapCtrl != null && _leap != null)
            {
                _leapCtrl.RemoveListener(_leap);
            }
        }

        public void gesture_Occur(object sender, EventArgs e)
        {
            SlideNext();
        }

        private void SlideNext()
        {
            if (_slideWindow != null)
            {
                if (_slideWindow.Active == Office.MsoTriState.msoTrue)
                {
                    var view = _slideWindow.View;
                    try
                    {
                        view.Next();
                        //view.GotoSlide(view.CurrentShowPosition + 1);
                    }
#if !DEBUG
                    catch(System.Runtime.InteropServices.COMException ex)
                    {
                        Console.WriteLine("AddIn error : " + ex.Message);
                    }
#endif
                    finally
                    { }
                }
                else
                {
                    Console.WriteLine("SlideWindow is not active.");
                }
            }
        }

        #region VSTO で生成されたコード

        /// <summary>
        /// デザイナーのサポートに必要なメソッドです。
        /// このメソッドの内容をコード エディターで変更しないでください。
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
