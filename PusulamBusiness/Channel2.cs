using System;

namespace PusulamBusiness
{
    public class Channel2<T> : IDisposable where T : DBase, new()
    {
        public readonly T _cs;
        public Channel2(int idMenu)
        {
            _cs = (T)Activator.CreateInstance(typeof(T));
            _cs.ID_MENU = idMenu;
        }

        //public T _cs;
        //public T _Cs
        //{
        //    get
        //    {
        //        _cs = (T)Activator.CreateInstance(typeof(T, new object[] { ID_MENU });
        //        return _cs;
        //    }
        //}

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
