// Copyright (c) 2014 shobomaru
// This software is released under the MIT license.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Leap;

namespace LeapSlides
{
    class LeapListener : Listener
    {
        public LeapListener()
        {
        }

        /// <summary>
        /// ジェスチャーを通知するイベント
        /// </summary>
        public event EventHandler GestureOccur;

        public override void OnInit(Controller ctrl)
        {
        }

        public override void OnConnect(Controller ctrl)
        {
            ctrl.EnableGesture(Leap.Gesture.GestureType.TYPESWIPE);
        }

        public override void OnDisconnect(Controller ctrl)
        {
        }

        #region Private gesture states

        class GestureParam
        {
            public int id;
            public DateTime time;
        }

        /// <summary>
        /// 動いている（ジェスチャーの最中の）指のIDのリスト
        /// </summary>
        List<GestureParam> _movingList = new List<GestureParam>();
        int _firstMovingId;

        #endregion

        public override void OnFrame(Controller ctrl)
        {
            Frame frame = ctrl.Frame();
            GestureList gestureList = frame.Gestures();

            DateTime currentDateTime = DateTime.Now;
            // Startイベントが来たのにStopイベントが来ない場合、一定時間を以て破棄する。
            int numRemoved = _movingList.RemoveAll(new Predicate<GestureParam>((GestureParam param) =>
            {
                return currentDateTime.AddMilliseconds(-500).CompareTo(param.time) > 0 ? true : false;
            }));
            // なんらかのStartイベントが起きていて、破棄されたことでリストが空になったら、Stop状態が起きたとみなしてイベントを発行する。
            if (numRemoved > 0 && _movingList.Count == 0 && _firstMovingId != 0)
            {
                GestureOccur(this, new EventArgs());
                _firstMovingId = 0;
            }

            for (int i = 0; i < gestureList.Count; i++)
            {
                Gesture gesture = gestureList[i];
                if (gesture.Type == Gesture.GestureType.TYPESWIPE)
                {
                    // ジェスチャーの開始
                    if (gesture.State == Gesture.GestureState.STATE_START)
                    {
                        Debug.WriteLine(string.Format("Gesture Start : ID={0}, State={1}, Type={2}", gesture.Id, gesture.State, gesture.Type));

                        // 本来あり得ない状態だが、同一IDが既に記憶済みならば中断する。
                        int findID = _movingList.FindIndex(new Predicate<GestureParam>((GestureParam param) =>
                        {
                            return (param.id == gesture.Id) ? true : false;
                        }));
                        if (findID != -1)
                        {
                            continue;
                        }

                        // 動作中の指を記憶する。
                        var p = new GestureParam();
                        p.id = gesture.Id;
                        p.time = currentDateTime;
                        _movingList.Add(p);

                        // 最初に動き出した指ならば、個別に記憶する。
                        _firstMovingId = gesture.Id;
                    }
                    // ジェスチャの終了
                    else if (gesture.State == Gesture.GestureState.STATE_STOP)
                    {
                        Debug.WriteLine(string.Format("Gesture Stop : ID={0}, State={1}, Type={2}", gesture.Id, gesture.State, gesture.Type));
                        int findID = _movingList.FindIndex(new Predicate<GestureParam>((GestureParam param) =>
                        {
                            return (param.id == gesture.Id) ? true : false;
                        }));
                        // Startイベントが来ておらず、いきなりStopが来たら、何もしない。
                        if (findID == -1)
                        {
                            continue;
                        }

                        // 最初に動き出した指が停止したら、イベントを通知する。
                        if (_firstMovingId == _movingList[findID].id)
                        {
                            GestureOccur(this, new EventArgs());
                            _firstMovingId = 0;
                        }
                        
                        _movingList.RemoveAt(findID);
                    }
                }
            }
        }
    }
}
