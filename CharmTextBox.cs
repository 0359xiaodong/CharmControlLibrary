#region �ĵ�˵��
/* ****************************************************************************************************
 * �ĵ����ߣ�����
 * �������ڣ�2012��10��6��
 * �ĵ���;��CharmTextBox - �ı���ؼ�
 * -----------------------------------------------------------------------------------------------------
 * �޸ļ�¼��
 * 2013-01-20�����ţ���
 *  - �������� cTexxBox �޸�Ϊ CharmTextBox
 *  - �淶���ֶΡ����Ե������ط���
 * 2013-01-21�����ţ����޸� �߿���ְ׿� ������
 * 2013-03-01�����CSBox�����׼2.0������������
 * -----------------------------------------------------------------------------------------------------
 * �ο����ף�
 * 
 * *****************************************************************************************************/
#endregion

#region �����ռ�����
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using CharmCommonMethod;
#endregion

namespace CharmControlLibrary
{
    #region ö������
    /// <summary>
    /// �ı�������ģʽ��ͨ����ʽ��ֻ����ʽ���������룬��������
    /// </summary>
    public enum InputMode : int
    {
        /// <summary>
        /// ͨ����ʽ�������û����ı���������в���
        /// </summary>
        Normal,
        /// <summary>
        /// ֻ����ʽ���������û������κ�����
        /// </summary>
        ReadOnly,
        /// <summary>
        /// �������룬�ı�������������ʽ����
        /// </summary>
        Password,
        /// <summary>
        /// �������룬ֻ�����û��������֣�0-9�����˸��
        /// </summary>
        Integer
    }
    #endregion

    /// <summary>
    /// ��ʾ CharmControlLibrary.CharmTextBox �ı���ؼ�
    /// </summary>
    public class CharmTextBox : PictureBox
    {
        #region �ֶ�
        // �ؼ����ı�������ģʽ
        private InputMode mInputMode;
        // �ı���ؼ�
        private TextBox mTextbox;
        // �ؼ���״̬
        private ControlStatus mControlStatus;
        // �ؼ���״̬ͼƬ
        private Image[] mStatusImages;
        // �ؼ���ˮӡ�ı�
        private string mWatermark;
        #endregion

        #region ����
        /// <summary>
        /// ��ȡ�����ÿؼ��Ŀ��
        /// </summary>
        public new int Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                // ���������ı����С
                mTextbox.Size = new Size(base.Width - 4, mTextbox.Height);
                mStatusImages[0] = ImageOperation.ResizeImageWithoutBorder(mStatusImages[0], 2, 1, 2, 1, base.Size);
                mStatusImages[1] = ImageOperation.ResizeImageWithoutBorder(mStatusImages[1], 2, 2, 2, 2, base.Size);
            }
        }

        /// <summary>
        /// ��ȡ�����ÿؼ����ı���ˮƽ���뷽ʽ
        /// </summary>
        public HorizontalAlignment TextAlign
        {
            get { return mTextbox.TextAlign; }
            set { mTextbox.TextAlign = value; }
        }

        /// <summary>
        /// ��ȡ�����ÿؼ����ı�������ģʽ
        /// </summary>
        public InputMode TextInputMode
        {
            get { return mInputMode; }
            set
            {
                mInputMode = value;

                // �����������Ϊֻ�����ͻ�������������ˮӡЧ��
                if (mInputMode == InputMode.ReadOnly || mInputMode == InputMode.Password)
                {
                    this.mWatermark = null;
                    if (mInputMode == InputMode.Password)
                        mTextbox.PasswordChar = '��';     // ��������
                }
            }
        }

        /// <summary>
        /// ��ȡ�����ÿؼ���ˮӡ�ı�
        /// </summary>
        public string Watermark
        {
            get { return this.mWatermark; }
            set
            {
                this.mWatermark = value;
                // �ж��ı��Ƿ�Ϊ��
                if (string.Equals(mTextbox.Text, string.Empty))
                {
                    mTextbox.ForeColor = Color.DarkGray;
                    mTextbox.Text = mWatermark;
                }
            }
        }

        /// <summary>
        /// ��ȡ�������û����ڿؼ������������ַ���
        /// </summary>
        public int MaxLength
        {
            get { return mTextbox.MaxLength; }
            set
            {
                // �ж��Ƿ�Ϊ�Ǹ�����
                if (value >= 0)
                    mTextbox.MaxLength = value;
                else
                    throw (new ArgumentException("MaxLength:�ı������������ַ��������ǷǸ�����."));
            }
        }

        /// <summary>
        /// ��ȡ�������ı��������
        /// </summary>
        public new string Text
        {
            get { return mTextbox.Text; }
            set { mTextbox.Text = value; }
        }
        #endregion

        #region �����¼�
        /// <summary>
        /// �ؼ��ػ��¼�
        /// </summary>
        protected override void OnPaint(PaintEventArgs pe)
        {
            // ��ȡ���ƶ���
            Graphics g = pe.Graphics;

            // �жϿؼ�״̬�����Ʊ���
            switch (mControlStatus)
            {
                case ControlStatus.Normal:
                    g.DrawImage(mStatusImages[0], new Rectangle(new Point(0, 0), mStatusImages[0].Size));
                    break;
                case ControlStatus.Hover:
                    g.DrawImage(mStatusImages[1], new Rectangle(new Point(0, 0), mStatusImages[1].Size));
                    break;
            }
        }
        #endregion

        #region �Զ����¼�
        /// <summary>
        /// �����ı���˫���¼�ί��
        /// </summary>
        public delegate void DoubleClickEventHandler(object sender, EventArgs e);
        /// <summary>
        /// ��˫���ؼ�ʱ����
        /// </summary>
        public new event DoubleClickEventHandler DoubleClick;

        /// <summary>
        /// �����ı����ı��ı��¼�ί��
        /// </summary>
        public delegate void TextChangedEventHandler(object sender, EventArgs e);
        /// <summary>
        /// ���ؼ��ı��ı�ʱ����
        /// </summary>
        public new event TextChangedEventHandler TextChanged;

        // �������¼�
        private void TextBox_MouseEnter(object sender, EventArgs e)
        {
            // �޸ı���
            this.mControlStatus = ControlStatus.Hover;
            this.Invalidate();

            // �ж��Ƿ���ˮӡ
            if (mWatermark != null)
            {
                // �ж��ı��Ƿ�ΪĬ���ı�
                if (string.Equals(mTextbox.Text, mWatermark))
                {
                    mTextbox.Text = string.Empty;
                    mTextbox.ForeColor = Color.Black;
                }
            }
        }

        // ����뿪�¼�
        private void TextBox_MouseLeave(object sender, EventArgs e)
        {
            // �޸ı���
            this.mControlStatus = ControlStatus.Normal;
            this.Invalidate();

            // �ж��Ƿ�ӵ�н�������ˮӡ
            if (!mTextbox.Focused)
            {
                if (mWatermark != null)
                {
                    // �ж��ı��Ƿ�Ϊ��
                    if (string.Equals(mTextbox.Text, string.Empty))
                    {
                        mTextbox.ForeColor = Color.DarkGray;
                        mTextbox.Text = mWatermark;
                    }
                }
            }
        }

        // ���̰����¼�
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // �ж���������
            switch (mInputMode)
            {
                case InputMode.Normal:
                    break;
                case InputMode.ReadOnly:
                    e.Handled = true;
                    break;
                case InputMode.Password:
                    break;
                case InputMode.Integer:
                    if (!(char.IsDigit(e.KeyChar) | e.KeyChar == '\b'))
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }

        // ��ý����¼�
        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            // �ж��Ƿ���ˮӡ
            if (mWatermark != null)
            {
                // �ж��ı��Ƿ�ΪĬ���ı�
                if (string.Equals(mTextbox.Text, mWatermark))
                {
                    mTextbox.Text = string.Empty;
                    mTextbox.ForeColor = Color.Black;
                }
            }
        }

        // ʧȥ�����¼�
        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            // �ж��Ƿ���ˮӡ
            if (mWatermark != null)
            {
                // �ж��ı��Ƿ�Ϊ��
                if (string.Equals(mTextbox.Text, string.Empty))
                {
                    mTextbox.ForeColor = Color.DarkGray;
                    mTextbox.Text = mWatermark;
                }
            }
        }
        #endregion

        #region �ؼ��¼�
        // �ı���˫���¼�
        private void mTextbox_DoubleClick(object sender, EventArgs e)
        {
            if (this.DoubleClick != null)
                this.DoubleClick(sender, e);
        }

        // �ı��ı��¼�
        private void mTextbox_TextChanged(object sender, EventArgs e)
        {
            if (this.TextChanged != null)
                this.TextChanged(sender, e);
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��ʼ�� CharmTextBox �����ʵ��
        /// </summary>
        public CharmTextBox()
            : base()
        {
            // * ��ʼ������ *
            this.Size = new Size(160, 26);
            this.BackColor = Color.White;
            this.mStatusImages = new Image[2];
            this.mStatusImages[0] = Properties.Resources.textbox_bkg_normal;
            // ȥ����̬�����հײ���
            this.mStatusImages[0] =
                ImageOperation.GetPartOfImage(mStatusImages[0], mStatusImages[0].Width, mStatusImages[0].Height - 2, 0, 1);
            this.mStatusImages[1] = Properties.Resources.textbox_bkg_hover;
            // ���������ı����С
            mStatusImages[0] = ImageOperation.ResizeImageWithoutBorder(mStatusImages[0], 2, 1, 2, 1, base.Size);
            mStatusImages[1] = ImageOperation.ResizeImageWithoutBorder(mStatusImages[1], 2, 2, 2, 2, base.Size);

            // * �ı��������ʽ���� *
            mTextbox = new TextBox();
            mTextbox.BorderStyle = BorderStyle.None; // �ޱ߿�
            mTextbox.Font = new Font("΢���ź�", 9.75F);   // ����
            mTextbox.Size = new Size(156, 20);    // ��С
            mTextbox.Location = new Point(2, (this.Height - mTextbox.Height) / 2);   // λ��
            mTextbox.ImeMode = ImeMode.NoControl; // ���뷨����
            // ����ˮӡ
            if (mWatermark != null)
            {
                mTextbox.ForeColor = Color.DarkGray;
                mTextbox.Text = mWatermark;
            }

            // * �ؼ��������� *
            mInputMode = InputMode.Normal;   // ����ģʽ

            // * �����ؼ��¼� *
            mTextbox.MouseEnter += new EventHandler(TextBox_MouseEnter);
            mTextbox.MouseLeave += new EventHandler(TextBox_MouseLeave);
            mTextbox.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);
            mTextbox.LostFocus += new EventHandler(TextBox_LostFocus);
            mTextbox.GotFocus += new EventHandler(TextBox_GotFocus);
            mTextbox.DoubleClick += new EventHandler(mTextbox_DoubleClick);
            mTextbox.TextChanged += new EventHandler(mTextbox_TextChanged);

            // * ���ؿؼ����ؼ������� *
            this.Controls.Add(mTextbox);
        }
        #endregion

        #region ����
        /// <summary>
        /// �����ı����ı���������ˮӡʱ������ʾ����
        /// </summary>
        /// <param name="text">�ı�����</param>
        public void SetText(string text)
        {
            mTextbox.ForeColor = Color.Black;
            mTextbox.Text = text;
        }
        #endregion
    }
}
