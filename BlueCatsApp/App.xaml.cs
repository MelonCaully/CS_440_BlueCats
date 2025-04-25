namespace BlueCatsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddPlayerPage), typeof(AddPlayerPage));
            Routing.RegisterRoute(nameof(MyPlayerPage), typeof(MyPlayerPage));
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}