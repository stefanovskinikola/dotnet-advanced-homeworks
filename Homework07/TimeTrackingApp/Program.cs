using TimeTrackingApp;
using TimeTrackingApp.Application.Abstractions.Persistence;
using TimeTrackingApp.Application.Abstractions.Security;
using TimeTrackingApp.Application.Abstractions.Services;
using TimeTrackingApp.Application.Services;
using TimeTrackingApp.Infrastructure.Persistence;
using TimeTrackingApp.Infrastructure.Persistence.Repositories;
using TimeTrackingApp.Infrastructure.Security;
using TimeTrackingApp.Ui.Flows;

var appDataPaths = new AppDataPaths();
var jsonFileStore = new JsonFileStore();

IUserRepository userRepository = new UserRepository(jsonFileStore, appDataPaths);
IActivityRepository activityRepository = new ActivityRepository(jsonFileStore, appDataPaths);
IPasswordHasher passwordHasher = new Pbkdf2PasswordHasher();
IUserValidationService userValidationService = new UserValidationService();
IAuthService authService = new AuthService(userRepository, userValidationService, passwordHasher);
IAccountService accountService = new AccountService(userRepository, userValidationService, passwordHasher);
IActivityTrackingService activityTrackingService = new ActivityTrackingService(activityRepository);
IStatisticsService statisticsService = new StatisticsService(activityRepository);

var authenticationFlow = new AuthenticationFlow(authService, userValidationService);
var trackingFlow = new TrackingFlow(activityTrackingService);
var statisticsFlow = new StatisticsFlow(statisticsService);
var accountManagementFlow = new AccountManagementFlow(accountService);
var mainMenuFlow = new MainMenuFlow(trackingFlow, statisticsFlow, accountManagementFlow);

var consoleApp = new TimeTrackingConsoleApp(
    authenticationFlow,
    mainMenuFlow);

await consoleApp.RunAsync();
