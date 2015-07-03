using Yis.Designer.Software.Business;

namespace Yis.Designer.Technic.Contract
{
    public interface IGenerator
    {
        #region Methods

        void Generate(NameSpace root, string outputDirectory);

        #endregion Methods
    }
}