using System;

namespace Utils.Templates.Disposable
{
    public class MyDisposable : IDisposable
    {
        private readonly IDisposable _someDisposable;

        public MyDisposable() { }
        public MyDisposable(IDisposable someDisposable)
        {
            _someDisposable = someDisposable;
        }


        #region IDisposable

        private bool _disposed;
        ~MyDisposable() => Dispose();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _someDisposable?.Dispose();

            _disposed = true;
        }

        #endregion IDisposable
    }
}