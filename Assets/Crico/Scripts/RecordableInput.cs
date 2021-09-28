using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Crico
{
    public class RecordableInput : BaseInput
    {
        static public RecordableInput instance { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            instance = this;
        }

        [Serializable]
        public class RecordedVarInstance<T>
        {
            public T value;
            public int frameCount;
        }

        [Serializable]
        public class SerializableVector2
        {
            public float x;
            public float y;

            public SerializableVector2() { }
            public SerializableVector2(Vector2 input)
            {
                this.x = input.x;
                this.y = input.y;
            }

            public Vector2 ConvertToVector2()
            {
                return new Vector2(x, y);
            }
        }

        [Serializable]
        public class RecordedVar<T>
        {
            public string name;
            public List<RecordedVarInstance<T>> listOfVars;
            private Func<T, T, bool> CompareEquals;

            private int debugLastIndex = -1;
            [NonSerialized] private Recording recordingParent;

            public RecordedVar() { }

            public RecordedVar(Recording recordingParent, string name, T firstVal, Func<T, T, bool> CompareEquals)
            {
                this.name = name;
                listOfVars = new List<RecordedVarInstance<T>>();
                RecordedVarInstance<T> instance = new RecordedVarInstance<T>();
                instance.value = firstVal;
                instance.frameCount = 0;
                listOfVars.Add(instance);
                this.recordingParent = recordingParent;

                this.CompareEquals = CompareEquals;
            }

            public T GetVar(int frameCount)
            {
                RecordedVarInstance<T> result = null;
                int index = 0;
                for (int i = 0; i < listOfVars.Count; ++i)
                {
                    RecordedVarInstance<T> recordedVarInstance = listOfVars[i];
                    if (recordedVarInstance.frameCount <= frameCount)
                    {
                        result = recordedVarInstance;
                        index = i;
                    }
                    else
                    {
                        break;
                    }
                }

                if (index != debugLastIndex)
                {
                    //Debug.Log(name + ": Returning: " + result.value + ", FrameCount: " + result.frameCount);
                    debugLastIndex = index;
                }

                return result.value;
            }

            public void SetVar(T value, int frameCount)
            {
                RecordedVarInstance<T> last = listOfVars[listOfVars.Count - 1];
                if (!CompareEquals(last.value, value))
                {
                    recordingParent.UpdateFinalFrame();
                    if (frameCount == last.frameCount)
                    {
                        last.value = value;
                    }
                    else
                    {
                        RecordedVarInstance<T> newVar = new RecordedVarInstance<T>();
                        newVar.value = value;
                        newVar.frameCount = frameCount;
                        listOfVars.Add(newVar);
                    }
                }
            }
        }

        [Serializable]
        public class RecordedVarString : RecordedVar<string> { public RecordedVarString(Recording recordingParent, string name, string firstVal, Func<string, string, bool> CompareEquals) : base(recordingParent, name, firstVal, CompareEquals) { } }
        [Serializable]
        public class RecordedVarBool : RecordedVar<bool> { public RecordedVarBool(Recording recordingParent, string name, bool firstVal, Func<bool, bool, bool> CompareEquals) : base(recordingParent, name, firstVal, CompareEquals) { } }
        [Serializable]
        public class RecordedVarVector2 : RecordedVar<SerializableVector2> { public RecordedVarVector2(Recording recordingParent, string name, SerializableVector2 firstVal, Func<SerializableVector2, SerializableVector2, bool> CompareEquals) : base(recordingParent, name, firstVal, CompareEquals) { } }
        [Serializable]
        public class RecordedVarInt : RecordedVar<int> { public RecordedVarInt(Recording recordingParent, string name, int firstVal, Func<int, int, bool> CompareEquals) : base(recordingParent, name, firstVal, CompareEquals) { } }

        [Serializable]
        public class Recording
        {
            //IMECompositionMode imeCompositionMode;
            //public RecordedVar<Vector2> compositionCursorPos;
            // public RecordedVarBool touchSupported;
            //float GetAxisRaw(string axisName);
            //bool GetButtonDown(string buttonName);
            //RecordedVar<Touch> GetTouch;

            //Touch GetTouch(int index);
            static public bool CompareString(string a, string b)
            {
                return a == b;
            }

            static public bool CompareVector2(SerializableVector2 a, SerializableVector2 b)
            {
                return a.x == b.x && a.y == b.y;
            }

            static public bool CompareBool(bool a, bool b)
            {
                return a == b;
            }

            static public bool CompareInt(int a, int b)
            {
                return a == b;
            }

            static public bool CompareFloat(float a, float b)
            {
                return a == b;
            }

            public int finalFrame;

            public RecordedVarString compositionString;
            public RecordedVarBool mousePresent;
            public RecordedVarVector2 mousePosition;
            public RecordedVarVector2 mouseScrollDelta;
            public RecordedVarInt touchCount;

            public Dictionary<KeyCode, RecordedVarBool> GetKeyDown;
            public Dictionary<KeyCode, RecordedVarBool> GetKeyUp;
            public Dictionary<KeyCode, RecordedVarBool> GetKey;

            public RecordedVarBool[] GetMouseButton;
            public RecordedVarBool[] GetMouseButtonDown;
            public RecordedVarBool[] GetMouseButtonUp;

            [NonSerialized] private RecordableInput parent;

            public Recording()
            {
            }

            public Recording(RecordableInput parent)
            {
                this.parent = parent;
            }

            public void UpdateFinalFrame()
            {
                parent.UpdateRecordingFinalFrame();
            }

            public void Init()
            {
                compositionString = new RecordedVarString(this, "compositionString", Input.compositionString, CompareString);
                mousePresent = new RecordedVarBool(this, "mousePresent", Input.mousePresent, CompareBool);
                mousePosition = new RecordedVarVector2(this, "mousePosition", new SerializableVector2(Input.mousePosition), CompareVector2);
                mouseScrollDelta = new RecordedVarVector2(this, "mouseScrollDelta", new SerializableVector2(Input.mouseScrollDelta), CompareVector2);
                touchCount = new RecordedVarInt(this, "touchCount", Input.touchCount, CompareInt);

                GetKeyDown = new Dictionary<KeyCode, RecordedVarBool>();
                GetKeyUp = new Dictionary<KeyCode, RecordedVarBool>();
                GetKey = new Dictionary<KeyCode, RecordedVarBool>();

                GetMouseButton = new RecordedVarBool[]
                {
                new RecordedVarBool(this, "GetMouseButton0", Input.GetMouseButton(0), CompareBool),
                new RecordedVarBool(this, "GetMouseButton1", Input.GetMouseButton(1), CompareBool),
                new RecordedVarBool(this, "GetMouseButton2", Input.GetMouseButton(2), CompareBool),
                };

                GetMouseButtonDown = new RecordedVarBool[]
                {
                new RecordedVarBool(this, "GetMouseButtonDown0", Input.GetMouseButtonDown(0), CompareBool),
                new RecordedVarBool(this, "GetMouseButtonDown1", Input.GetMouseButtonDown(1), CompareBool),
                new RecordedVarBool(this, "GetMouseButtonDown2", Input.GetMouseButtonDown(2), CompareBool),
                };

                GetMouseButtonUp = new RecordedVarBool[]
                {
                new RecordedVarBool(this, "GetMouseButtonUp0", Input.GetMouseButtonUp(0), CompareBool),
                new RecordedVarBool(this, "GetMouseButtonUp1", Input.GetMouseButtonUp(1), CompareBool),
                new RecordedVarBool(this, "GetMouseButtonUp2", Input.GetMouseButtonUp(2), CompareBool),
                };
            }
        }

        [SerializeField] string filenameFormat = "/inputRecording{0}.cir";
        [SerializeField] string playbackFile = "/inputRecording0.cir";
        [SerializeField] bool respondToHotKeys = true;
        [SerializeField] bool record;
        [SerializeField] bool play;

        bool doingRecording;
        Recording recordingData;
        int startFrame;
        int currentFrameCount { get => (Time.frameCount - startFrame); }

        bool doingPlaying;

        private void Update()
        {
            if (respondToHotKeys && Input.GetKeyDown(KeyCode.R))
            {
                play = false;
                record = !record;
            }

            if (respondToHotKeys && Input.GetKeyDown(KeyCode.P))
            {
                record = false;
                play = !play;
            }

            UpdateRecordingStartStop();
            UpdatePlayingStartStop();
        }

        private void UpdateRecordingStartStop()
        {
            if (doingRecording)
            {
                if (!record)
                {
                    StopRecording();
                }
            }
            else
            {
                if (record)
                {
                    if (doingPlaying)
                        StopPlayback();

                    StartRecording();
                }
            }

        }

        private void StartRecording()
        {
            doingRecording = true;
            recordingData = new Recording(this);
            recordingData.Init();
            startFrame = Time.frameCount;
        }

        private void StopRecording()
        {
            record = false;
            if (!doingRecording)
                return;

            doingRecording = false;

            SaveRecordingData();
            recordingData = null;
        }

        public void UpdateRecordingFinalFrame()
        {
            recordingData.finalFrame = currentFrameCount;
        }

        private void SaveRecordingData()
        {
            const int LIMIT = 10000;
            int index = 0;
            string filePath = Application.persistentDataPath + string.Format(filenameFormat, index);
            while (File.Exists(filePath) && index < LIMIT)
            {
                ++index;
                filePath = Application.persistentDataPath + string.Format(filenameFormat, index);
            }

            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Create(filePath);
            bf.Serialize(file, recordingData);

            file.Close();

            Debug.Log("Saved RecordedInput File: " + filePath);
            playbackFile = string.Format(filenameFormat, index);
        }


        private void UpdatePlayingStartStop()
        {
            if (doingPlaying)
            {
                if (!play)
                {
                    StopPlayback();
                }
                else
                {
                    if (currentFrameCount > recordingData.finalFrame)
                    {
                        Debug.Log("Finished playback: " + playbackFile);
                        StopPlayback();
                    }
                }
            }
            else
            {
                if (play)
                {
                    if (doingRecording)
                        StopRecording();

                    StartPlayback();
                }
            }
        }

        private void StopPlayback()
        {
            play = false;

            if (!doingPlaying)
                return;

            doingPlaying = false;
            Debug.Log("Stopped playback: " + playbackFile);
        }

        private void StartPlayback()
        {
            startFrame = Time.frameCount;

            if (File.Exists(Application.persistentDataPath + playbackFile))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + playbackFile, FileMode.Open);
                recordingData = (Recording)bf.Deserialize(file);
                file.Close();

                doingPlaying = true;

                Debug.Log("Loaded input playback file: " + playbackFile);
            }
            else
            {
                play = false;
                Debug.LogError("Input playback file not found: " + playbackFile);
            }
        }

        public override string compositionString
        {
            get
            {
                string result = base.compositionString;

                if (doingRecording)
                    recordingData.compositionString.SetVar(result, currentFrameCount);

                if (doingPlaying)
                    result = recordingData.compositionString.GetVar(currentFrameCount);

                return result;
            }
        }

        public override bool mousePresent //=> base.mousePresent;
        {
            get
            {
                bool result = base.mousePresent;

                if (doingRecording)
                    recordingData.mousePresent.SetVar(result, currentFrameCount);

                if (doingPlaying)
                    result = recordingData.mousePresent.GetVar(currentFrameCount);

                return result;
            }
        }

        public override Vector2 mousePosition// => base.mousePosition;
        {
            get
            {
                Vector2 result = base.mousePosition;

                if (doingRecording)
                    recordingData.mousePosition.SetVar(new SerializableVector2(result), currentFrameCount);

                if (doingPlaying)
                    result = recordingData.mousePosition.GetVar(currentFrameCount).ConvertToVector2();

                return result;
            }
        }

        public override Vector2 mouseScrollDelta// => base.mouseScrollDelta;
        {
            get
            {
                Vector2 result = base.mouseScrollDelta;

                if (doingRecording)
                    recordingData.mouseScrollDelta.SetVar(new SerializableVector2(result), currentFrameCount);

                if (doingPlaying)
                    result = recordingData.mouseScrollDelta.GetVar(currentFrameCount).ConvertToVector2();

                return result;
            }
        }

        public override int touchCount// => base.touchCount;
        {
            get
            {
                int result = base.touchCount;

                if (doingRecording)
                    recordingData.touchCount.SetVar(result, currentFrameCount);

                if (doingPlaying)
                    result = recordingData.touchCount.GetVar(currentFrameCount);

                return result;
            }
        }

        public override bool GetMouseButton(int button)
        {
            Assert.IsTrue(button >= 0 && button <= 2);

            bool result = base.GetMouseButton(button);

            if (doingRecording)
                recordingData.GetMouseButton[button].SetVar(result, currentFrameCount);

            if (doingPlaying)
                result = recordingData.GetMouseButton[button].GetVar(currentFrameCount);

            return result;
        }

        public override bool GetMouseButtonDown(int button)
        {
            Assert.IsTrue(button >= 0 && button <= 2);

            bool result = base.GetMouseButtonDown(button);

            if (doingRecording)
                recordingData.GetMouseButtonDown[button].SetVar(result, currentFrameCount);

            if (doingPlaying)
                result = recordingData.GetMouseButtonDown[button].GetVar(currentFrameCount);

            return result;
        }

        public override bool GetMouseButtonUp(int button)
        {
            Assert.IsTrue(button >= 0 && button <= 2);

            bool result = base.GetMouseButtonUp(button);

            if (doingRecording)
                recordingData.GetMouseButtonUp[button].SetVar(result, currentFrameCount);

            if (doingPlaying)
                result = recordingData.GetMouseButtonUp[button].GetVar(currentFrameCount);

            return result;
        }

        public bool GetKeyDown(KeyCode keyCode)
        {
            bool result = Input.GetKeyDown(keyCode);

            if (doingRecording)
            {
                RecordedVarBool keyVal = null;
                if (!recordingData.GetKeyDown.TryGetValue(keyCode, out keyVal))
                {
                    keyVal = new RecordedVarBool(recordingData, keyCode.ToString(), false, Recording.CompareBool);
                    recordingData.GetKeyDown.Add(keyCode, keyVal);
                }

                keyVal.SetVar(result, currentFrameCount);
            }

            if (doingPlaying)
            {
                RecordedVarBool keyVal = null;
                if (recordingData.GetKeyDown.TryGetValue(keyCode, out keyVal))
                    result = keyVal.GetVar(currentFrameCount);
            }

            return result;
        }

        public bool GetKeyUp(KeyCode keyCode)
        {
            bool result = Input.GetKeyUp(keyCode);

            if (doingRecording)
            {
                RecordedVarBool keyVal = null;
                if (!recordingData.GetKeyUp.TryGetValue(keyCode, out keyVal))
                {
                    keyVal = new RecordedVarBool(recordingData, keyCode.ToString(), false, Recording.CompareBool);
                    recordingData.GetKeyUp.Add(keyCode, keyVal);
                }

                keyVal.SetVar(result, currentFrameCount);
            }

            if (doingPlaying)
            {
                RecordedVarBool keyVal = null;
                if (recordingData.GetKeyUp.TryGetValue(keyCode, out keyVal))
                    result = keyVal.GetVar(currentFrameCount);
            }

            return result;
        }

        public bool GetKey(KeyCode keyCode)
        {
            bool result = Input.GetKey(keyCode);

            if (doingRecording)
            {
                RecordedVarBool keyVal = null;
                if (!recordingData.GetKey.TryGetValue(keyCode, out keyVal))
                {
                    keyVal = new RecordedVarBool(recordingData, keyCode.ToString(), false, Recording.CompareBool);
                    recordingData.GetKey.Add(keyCode, keyVal);
                }

                keyVal.SetVar(result, currentFrameCount);
            }

            if (doingPlaying)
            {
                RecordedVarBool keyVal = null;
                if (recordingData.GetKey.TryGetValue(keyCode, out keyVal))
                    result = keyVal.GetVar(currentFrameCount);
            }

            return result;
        }


    }

}
