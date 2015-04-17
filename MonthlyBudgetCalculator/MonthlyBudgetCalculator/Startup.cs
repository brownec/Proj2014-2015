using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MonthlyBudgetCalculator.Startup))]
namespace MonthlyBudgetCalculator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
