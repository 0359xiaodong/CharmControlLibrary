#region �ĵ�˵��
/* ****************************************************************************************************
 * �ĵ����ߣ�����
 * �������ڣ�2012��10��6��
 * �ĵ���;��ͨ��ͼƬ��ʵ�ָ��Ѻõ� Button �ؼ���ʽ�Լ������Զ���Ŀؼ�����
 * -----------------------------------------------------------------------------------------------------
 * �޸ļ�¼��
 * 2012-10-18�����ţ����������� cButton �޸�Ϊ CharmButton
 * 2012-10-19�����ţ���
 *  - �淶���ֶΡ����Ե������ط���
 *  - ���� ��ť�ı���󳤶����ơ���⼰�쳣�׳�
 * 2012-10-23�����ţ����涨��갴�¡������¼�ֻ��Ӧ������
 * 2012-11-10�����ţ���ȡ�� ��ť�ı���󳤶����Ƶļ�⼰�쳣�׳�
 * 2013-01-21�����ţ���ȡ���Զ��嵥���¼�������Ĭ�ϵĵ����¼�ί��
 * 2013-01-23�����ţ���
 *  - ���� ���� Enabled
 *  - ȡ�� ���� Color
 * -----------------------------------------------------------------------------------------------------
 * �ο����ף�
 * C#�ؼ��Ի棺http://www.3600gz.cn/thread-122184-1-2.html
 * *****************************************************************************************************/
#endregion

#region �����ռ�����
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
#endregion

namespace CharmControlLibrary
{
    /// <summary>
    /// CharmButton����ָ�������ı�����ť��ʽ����ť���ͣ���ť״̬���ı���ɫ������
    /// </summary>
    public class CharmButton : PictureBox
    {
        #region ö������
        /// <summary>
        /// ��ť���ͣ�����ָ����ť��С�Ͱ�ť��ʽ��
        /// </summary>
        public enum ButtonType : int
        {
            /// <summary>
            /// ��ť��ʽ����СΪ 69*22
            /// </summary>
            Size_06922,
            /// <summary>
            /// ��ť��ʽ����СΪ 88*23
            /// </summary>
            Size_08223,
            /// <summary>
            /// ��ť��ʽ����СΪ 124*25
            /// </summary>
            Size_12425,
            /// <summary>
            /// �û��Զ��尴ť�ĸ�״̬��ͼƬ�ʹ�С
            /// </summary>
            Customize
        }

        /// <summary>
        /// ��ť״̬����̬���������̬����갴��̬��ʧ��̬
        /// </summary>
        public enum ButtonState : int
        {
            /// <summary>
            /// ��̬�����������
            /// </summary>
            Normal,
            /// <summary>
            /// �������̬��������ڿؼ��Ϸ�ʱ
            /// </summary>
            Hover,
            /// <summary>
            /// ��갴��̬����갴�¿ؼ�ʱ
            /// </summary>
            Down,
            /// <summary>
            /// ʧ��̬���ؼ�����ֹʱ
            /// </summary>
            Unenabled
        }
        #endregion

        #region �ֶ�
        private string _text; // �����ı�
        private int _textLength;    // �����ı�����
        private Point _position; // �ؼ�λ��
        private Color textColor;   // �ı���ɫ
        private ButtonType buttonType;  // ��ť����
        private ButtonState buttonState; // ��ť״̬
        private bool enabled = true;      // ��ť����״̬
        private Image _imgNormal; // ��ť��̬��ʽ
        private Image _imgHover;   // ��ť�������̬��ʽ
        private Image _imgDown;    // ��ť��갴��̬��ʽ
        private Image _imgUnenabled;   // ��ťʧ��̬��ʽ
        #endregion

        #region ����
        /// <summary>
        /// ��ȡ������ CharmControlLibrary.CharmButton �ı����ı�
        /// </summary>
        public override string Text
        {
            get { return this._text; }
            set
            {
                CheckTextLength(value);
                this._text = value;
                DrawImage();
            }
        }

        /// <summary>
        /// ��ȡ������ CharmControlLibrary.CharmButton �Ŀؼ�λ��
        /// </summary>
        public Point Position
        {
            get { return this._position; }
            set { this._position = value; }
        }

        /// <summary>
        /// ��ȡ������һ��ֵ����ֵָʾ�ؼ��Ƿ���Զ��û�����������Ӧ
        /// </summary>
        public new bool Enabled
        {
            get { return this.enabled; }
            set
            {
                this.enabled = value;
                base.Enabled = value;
                if (value)
                    buttonState = ButtonState.Normal;
                DrawImage();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ������һ��ӵ��ָ�������ı����ؼ�λ�ã���ť���ͣ��ı���ɫ�� CharmButton
        /// <param name="text">�����ı�</param>
        /// <param name="point">�ؼ�λ��</param>
        /// <param name="buttonType">��ť����</param>
        /// <param name="color">��ť�ı���ɫ</param>
        /// </summary>
        public CharmButton(
            string text,
            Point point,
            ButtonType buttonType,
            Color color)
        {
            CheckTextLength(text);
            this._text = text;
            this._position = point;
            this.buttonType = buttonType;
            // ���ݰ�ť����ָ����ť��ʽ
            if (buttonType == ButtonType.Size_06922)
            {
                _imgNormal = Properties.Resources.btn_06922_normal;
                _imgHover = Properties.Resources.btn_06922_hover;
                _imgDown = Properties.Resources.btn_06922_down;
                _imgUnenabled = Properties.Resources.btn_06922_unenabled;
            }
            else if (buttonType == ButtonType.Size_08223)
            {
                _imgNormal = Properties.Resources.btn_08223_normal;
                _imgHover = Properties.Resources.btn_08223_hover;
                _imgDown = Properties.Resources.btn_08223_down;
                _imgUnenabled = Properties.Resources.btn_08223_unenabled;
            }
            else if (buttonType == ButtonType.Size_12425)
            {
                _imgNormal = Properties.Resources.btn_12425_normal;
                _imgHover = Properties.Resources.btn_12425_hover;
                _imgDown = Properties.Resources.btn_12425_down;
                _imgUnenabled = Properties.Resources.btn_12425_Unenabled;
            }
            // ��ָ����ť״̬��Ĭ��Ϊ��̬
            this.buttonState = ButtonState.Normal;
            this.textColor = color;
            InitializeSetting();
        }

        /// <summary>
        /// ������һ��ӵ��ָ�������ı����ؼ�λ�ã���ť���ͣ��ı���ɫ����ť״̬���Զ�����ʽ��Դ�� CharmButton
        /// </summary>
        /// <param name="text">�����ı�</param>
        /// <param name="point">�ؼ�λ��</param>
        /// <param name="buttonType">��ť����</param>
        /// <param name="color">�ı���ɫ</param>
        /// <param name="buttonState">��ť״̬</param>
        /// <param name="imgNormal">��̬��ʽ��Դ</param>
        /// <param name="imgHover">�������̬��ʽ��Դ</param>
        /// <param name="imgDown">��갴��̬��ʽ��Դ</param>
        /// <param name="imgUnenabled">ʧ��̬��ʽ��Դ</param>
        public CharmButton(
            string text,
            Point point,
            ButtonType buttonType,
            Color color,
            ButtonState buttonState,
            Image imgNormal,
            Image imgHover,
            Image imgDown,
            Image imgUnenabled)
        {
            CheckTextLength(text);
            this._text = text;
            this._position = point;
            this.buttonType = buttonType;
            this.textColor = color;
            this.buttonState = buttonState;
            this._imgNormal = imgNormal;
            this._imgHover = imgHover;
            this._imgDown = imgDown;
            this._imgUnenabled = imgUnenabled;
            InitializeSetting();
        }

        /// <summary>
        /// ��ʼ���趨����
        /// </summary>
        private void InitializeSetting()
        {
            // * ��ť�����ʽ���� *
            base.Location = _position;
            // �ؼ���С����
            switch (buttonType)
            {
                case ButtonType.Size_06922:
                    base.Size = new Size(69, 22);
                    break;
                case ButtonType.Size_08223:
                    base.Size = new Size(82, 23);
                    break;
                case ButtonType.Size_12425:
                    base.Size = new Size(124, 25);
                    break;
                case ButtonType.Customize:
                    base.Size = _imgNormal.Size;
                    break;
            }
            base.BackColor = Color.Transparent;
            DrawImage();
            // * ��ť�����ʽ���� *
        }

        /// <summary>
        /// ����ı����ȷ��ϵ�ǰ��ť��ʽ
        /// </summary>
        /// <param name="text">Ҫ�����ı��ַ���</param>
        private void CheckTextLength(string text)
        {
            using (Graphics g = Graphics.FromImage(new Bitmap(this.Width, this.Height)))
            {
                SizeF sf = g.MeasureString(text, new Font("΢���ź�", 9));
                _textLength = Convert.ToInt32(sf.Width);
            }
        }
        #endregion

        #region �����¼�
        /// <summary>
        /// �������¼�
        /// </summary>
        protected override void OnMouseEnter(EventArgs e)
        {
            if (enabled)
            {
                buttonState = ButtonState.Hover;
                DrawImage();
            }
        }

        /// <summary>
        /// ����뿪�¼�
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (enabled)
            {
                buttonState = ButtonState.Normal;
                DrawImage();
            }
        }

        /// <summary>
        /// ��갴���¼�
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            // ֻ��Ӧ���
            if (mevent.Button == MouseButtons.Left && enabled)
            {
                buttonState = ButtonState.Down;
                DrawImage();
            }
        }

        /// <summary>
        /// ��굯���¼�
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            // ֻ��Ӧ���
            if (mevent.Button == MouseButtons.Left && enabled)
            {
                buttonState = ButtonState.Normal;
                DrawImage();
            }
        }

        // ��ͼ����
        private void DrawImage()
        {
            Color color = textColor; // �ı���ɫ
            int offsetX = 0;  // X����ƫ��
            int offsetY = 3;  // Y����ƫ��

            // �жϰ�ť����״̬
            if (!enabled)
                buttonState = ButtonState.Unenabled;

            // �жϰ�ť״̬
            switch (buttonState)
            {
                case ButtonState.Normal:
                    base.Image = new Bitmap(_imgNormal);
                    break;
                case ButtonState.Hover:
                    base.Image = new Bitmap(_imgHover);
                    break;
                case ButtonState.Down:
                    base.Image = new Bitmap(_imgDown);
                    offsetX = 1;
                    offsetY = 4;
                    break;
                case ButtonState.Unenabled:
                    base.Image = new Bitmap(_imgUnenabled);
                    color = Color.DarkGray;
                    break;
            }
            using (Graphics g = Graphics.FromImage(base.Image))
            {
                g.DrawString(_text, new Font("΢���ź�", 9), new SolidBrush(color),
                    new Point((base.Width / 2) - (_textLength / 2) + offsetX - 1, offsetY));
            }
        }

        /// <summary>
        /// ���������µİ�ť״̬���ı���ɫ
        /// </summary>
        /// <param name="buttonState">��ť״̬</param>
        /// <param name="color">�ı���ɫ</param>
        public void SetState(ButtonState buttonState, Color color)
        {
            this.textColor = color;
            this.buttonState = buttonState;
            DrawImage();
        }
        #endregion
    }
}
