namespace MvcSalesApp.DependencyResolution
{
  using Data;
  using StructureMap;
  using StructureMap.Graph;
  using System.Data.Entity;
  public class DefaultRegistry : Registry
  {
    #region Constructors and Destructors

    public DefaultRegistry() {
      Scan(
          scan =>
          {
            scan.TheCallingAssembly();
            scan.WithDefaultConventions();
            scan.With(new ControllerConvention());
          });
      For<DbContext>().Use<OrderSystemContext>().Transient();
    }

    #endregion Constructors and Destructors
  }
}