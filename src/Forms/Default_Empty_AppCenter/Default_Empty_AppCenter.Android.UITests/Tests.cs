using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Default_Empty_AppCenter.Android.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void NavigateTestOneView()
        {
            // Arrange
            Func<AppQuery, AppQuery> button = c => c.Marked("btnNavigateViewOne");
            Func<AppQuery, AppQuery> lbl = c => c.Marked("lblInfo").Text("Initialized");

            // Act
            app.Tap(button);

            AppResult[] result = app.Query(lbl);
            Assert.IsTrue(result.Any(), "The navigation didn't work.");
        }
    }
}
