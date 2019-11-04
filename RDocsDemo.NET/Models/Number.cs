using System;


namespace RDocsDemo.NET.Models
{
    public class Number : IComparable<Number>
    {
        private int value = 0;
        private string text = "";

        public int Value { get { return value; } }
        public string Text { get { return text; } }

        public Number(int v, string l)
        {
            value = v;
            text = l;
        }

        public override string ToString()
        {
            return Value + "\t" + Text;
        }

        public int CompareTo(Number other)
        {
            return Text.CompareTo(other.Text);
        }
    }
}
