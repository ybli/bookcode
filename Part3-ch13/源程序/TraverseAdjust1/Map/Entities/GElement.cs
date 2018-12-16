using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Map.Entities
{
    /*
    * 功能概要：图元虚类，各种点、线等实体类都从其派生
    * 编号：Map_Entities_001
    * 作者：廖振修
    *  创建日期:2016-06-09
    */
    public abstract class GElement
    {
        private string mName;
        private Color mColor;

        /// <summary>
        /// 图元名
        /// </summary>
        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }

        /// <summary>
        /// 图元颜色
        /// </summary>
        public Color Color
        {
            get
            {
                return mColor;
            }
            set
            {
                mColor = value;
            }
        }

        /// <summary>
        /// 绘制图元对象
        /// </summary>
        /// <param name="g"></param>
        public abstract void Draw(Graphics g);
    }
}//endSpace
