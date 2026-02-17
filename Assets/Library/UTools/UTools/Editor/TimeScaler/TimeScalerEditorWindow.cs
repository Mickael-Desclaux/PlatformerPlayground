using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Libraries.UTools.Editor
{
    public class TimeScalerEditorWindow : EditorWindow
    {
        private Label _valueLabel;
        private Label _headerLabel;
        private Slider _timeSlider;
        private Button _pauseButton;
        
        private VisualElement _mainContainer;
        private VisualElement _leftPane;
        private VisualElement _rightPane;

        [MenuItem("Window/UTools/TimeScaler")]
        public static void ShowWindow()
        {
            TimeScalerEditorWindow window = GetWindow<TimeScalerEditorWindow>();
            window.titleContent = new GUIContent("Time Control");
            window.minSize = new Vector2(250, 150);
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            root.style.backgroundColor = new StyleColor(new Color(0.18f, 0.18f, 0.18f));

            ScrollView scrollView = new();
            scrollView.style.flexGrow = 1;
            root.Add(scrollView);

            _mainContainer = new VisualElement();
            _mainContainer.style.paddingTop = 10;
            _mainContainer.style.paddingBottom = 10;
            _mainContainer.style.paddingLeft = 10;
            _mainContainer.style.paddingRight = 10;
            scrollView.Add(_mainContainer);

            CreateLeftPane();
            CreateRightPane();

            _mainContainer.Add(_leftPane);
            _mainContainer.Add(_rightPane);

            root.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);

            UpdateVisuals();
        }

        private void CreateLeftPane()
        {
            _leftPane = new VisualElement();
            _leftPane.style.marginBottom = 10;

            VisualElement headerContainer = new();
            headerContainer.style.backgroundColor = new StyleColor(new Color(0.13f, 0.13f, 0.13f));
            headerContainer.style.paddingTop = 10;
            headerContainer.style.paddingBottom = 10;
            headerContainer.style.marginBottom = 10;
            headerContainer.style.borderBottomWidth = 2;
            headerContainer.style.borderBottomColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f));

            _headerLabel = new Label("CURRENT TIME SCALE");
            _headerLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
            _headerLabel.style.fontSize = 10;
            _headerLabel.style.color = new StyleColor(Color.gray);
            
            _valueLabel = new Label($"{Time.timeScale:F2}x");
            _valueLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
            _valueLabel.style.fontSize = 26;
            _valueLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
            _valueLabel.style.color = new StyleColor(new Color(0.3f, 0.8f, 1f));

            headerContainer.Add(_headerLabel);
            headerContainer.Add(_valueLabel);
            _leftPane.Add(headerContainer);

            // 2. SLIDER & PAUSE
            VisualElement controlRow = new();
            controlRow.style.flexDirection = FlexDirection.Row;
            controlRow.style.marginBottom = 10;

            _pauseButton = new Button(TogglePause);
            _pauseButton.text = "II"; 
            _pauseButton.style.width = 35;
            _pauseButton.style.height = 25;
            _pauseButton.style.unityFontStyleAndWeight = FontStyle.Bold;
            _pauseButton.style.marginRight = 5;
            controlRow.Add(_pauseButton);

            _timeSlider = new Slider(0f, 5f);
            _timeSlider.value = Time.timeScale;
            _timeSlider.style.flexGrow = 1;
            _timeSlider.style.alignSelf = Align.Center;
            _timeSlider.RegisterValueChangedCallback(evt => SetTimeScale(evt.newValue));
            controlRow.Add(_timeSlider);

            _leftPane.Add(controlRow);

            Button resetButton = new(() => SetTimeScale(1.0f));
            resetButton.text = "RESET";
            resetButton.style.height = 25;
            resetButton.style.backgroundColor = new StyleColor(new Color(0.25f, 0.25f, 0.25f));
            _leftPane.Add(resetButton);
        }

        private void CreateRightPane()
        {
            _rightPane = new VisualElement();
            
            Label presetsLabel = new("Presets");
            presetsLabel.style.fontSize = 11;
            presetsLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
            presetsLabel.style.marginBottom = 5;
            presetsLabel.style.color = new StyleColor(new Color(0.7f, 0.7f, 0.7f));
            _rightPane.Add(presetsLabel);

            VisualElement presetsContainer = new();
            presetsContainer.style.flexDirection = FlexDirection.Row;
            presetsContainer.style.flexWrap = Wrap.Wrap; 
            presetsContainer.style.marginBottom = 10;

            AddPresetButton(presetsContainer, "0.1x", 0.1f);
            AddPresetButton(presetsContainer, "0.5x", 0.5f);
            AddPresetButton(presetsContainer, "1x", 1f);
            AddPresetButton(presetsContainer, "2x", 2f);
            AddPresetButton(presetsContainer, "5x", 5f);
            AddPresetButton(presetsContainer, "10x", 10f);

            _rightPane.Add(presetsContainer);

            Label fineTuneLabel = new("Fine Tuning");
            fineTuneLabel.style.fontSize = 11;
            fineTuneLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
            fineTuneLabel.style.marginBottom = 5;
            fineTuneLabel.style.color = new StyleColor(new Color(0.7f, 0.7f, 0.7f));
            _rightPane.Add(fineTuneLabel);

            VisualElement stepContainer = new();
            stepContainer.style.flexDirection = FlexDirection.Row;
            stepContainer.style.backgroundColor = new StyleColor(new Color(0.15f, 0.15f, 0.15f));
            stepContainer.style.paddingTop = 5;
            stepContainer.style.paddingBottom = 5;
            stepContainer.style.justifyContent = Justify.SpaceAround;

            AddStepButton(stepContainer, "-1", -1f);
            AddStepButton(stepContainer, "-0.1", -0.1f);
            AddStepButton(stepContainer, "+0.1", 0.1f);
            AddStepButton(stepContainer, "+1", 1f);

            _rightPane.Add(stepContainer);
        }

        private void OnGeometryChange(GeometryChangedEvent evt)
        {
            float width = evt.newRect.width;
            float height = evt.newRect.height;

            bool isLandscape = width > 380; 

            if (isLandscape)
            {
                _mainContainer.style.flexDirection = FlexDirection.Row;
                
                _leftPane.style.width = Length.Percent(45);
                _leftPane.style.marginRight = Length.Percent(5);
                _leftPane.style.marginBottom = 0;

                _rightPane.style.width = Length.Percent(50);
            }
            else
            {
                _mainContainer.style.flexDirection = FlexDirection.Column;
                
                _leftPane.style.width = Length.Percent(100);
                _leftPane.style.marginRight = 0;
                _leftPane.style.marginBottom = 15;

                _rightPane.style.width = Length.Percent(100);
            }
        }
        

        private void AddPresetButton(VisualElement parent, string label, float scale)
        {
            Button button = new(() => SetTimeScale(scale));
            button.text = label;
            button.style.flexGrow = 1; 
            button.style.minWidth = 40; 
            button.style.marginBottom = 2;
            parent.Add(button);
        }

        private void AddStepButton(VisualElement parent, string label, float step)
        {
            Button button = new(() => SetTimeScale(Time.timeScale + step));
            button.text = label;
            button.style.minWidth = 30;
            button.style.flexGrow = 1;
            button.style.unityFontStyleAndWeight = FontStyle.Bold;
            parent.Add(button);
        }

        private void TogglePause()
        {
            SetTimeScale(Time.timeScale == 0 ? 1f : 0f);
        }

        private void SetTimeScale(float scale)
        {
            float clampedScale = Mathf.Clamp(scale, 0f, 100f);
            Time.timeScale = clampedScale;
            TimeScaler.Scale = clampedScale;
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            if (_valueLabel == null) return;

            _valueLabel.text = $"{Time.timeScale:F2}x";
            _timeSlider.SetValueWithoutNotify(Time.timeScale);

            if (Time.timeScale == 0)
            {
                _valueLabel.style.color = new StyleColor(new Color(1f, 0.4f, 0.4f)); 
                _headerLabel.text = "GAME PAUSED";
                _pauseButton.text = "▶";
                _pauseButton.style.backgroundColor = new StyleColor(new Color(0.3f, 0.6f, 0.3f)); 
            }
            else
            {
                _headerLabel.text = "CURRENT TIME SCALE";
                _pauseButton.text = "II";
                _pauseButton.style.backgroundColor = new StyleColor(Color.clear); 

                if (Time.timeScale > 1.05f) 
                    _valueLabel.style.color = new StyleColor(new Color(0.4f, 0.8f, 1f)); 
                else if (Time.timeScale < 0.95f) 
                    _valueLabel.style.color = new StyleColor(new Color(1f, 0.8f, 0.4f)); 
                else 
                    _valueLabel.style.color = new StyleColor(new Color(0.4f, 1f, 0.6f)); 
            }
        }

        private void Update()
        {
            if (!EditorApplication.isPlaying) 
                return;
            
            if (Mathf.Abs(_timeSlider.value - Time.timeScale) > 0.01f)
                UpdateVisuals();
        }
    }
}