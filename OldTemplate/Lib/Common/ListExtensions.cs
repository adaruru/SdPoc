namespace OldTemplate.Lib.Common
{
    public class NavigationList<T> : List<T>
    {
        private int _currentIndex = 0;
        public int CurrentIndex
        {
            get
            {
                if (_currentIndex > Count - 1) { _currentIndex = Count - 1; }
                if (_currentIndex < 0) { _currentIndex = 0; }
                return _currentIndex;
            }
            set { _currentIndex = value; }
        }

        public bool MoveNext
        {
            get { return _currentIndex++ > Count - 1; }
        }

        public bool MovePrevious
        {
            get { return _currentIndex-- >= 0; }
        }

        public T Current
        {
            get { return this[CurrentIndex]; }
        }

    }
}
