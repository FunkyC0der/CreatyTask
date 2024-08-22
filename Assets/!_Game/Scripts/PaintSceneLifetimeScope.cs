using CreatyTest.Painting.Paintables;
using CreatyTest.Painting.PaintTools;
using VContainer;
using VContainer.Unity;

namespace CreatyTest
{
  public class PaintSceneLifetimeScope : LifetimeScope
  {
    public PaintableService PaintableService;
    public PaintToolService PaintToolService;
    
    protected override void Configure(IContainerBuilder builder)
    {
      builder.RegisterComponent(PaintableService);
      builder.RegisterComponent(PaintToolService);
    }
  }
}