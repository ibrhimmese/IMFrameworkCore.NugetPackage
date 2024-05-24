# Dependency
 Created by İBRAHİM MEŞE .NET8

## Install
 Kurulum için bu kodu kullanın

## Services

```Csharp
 public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //UseAutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //UseBusinessException
        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(),typeof(BaseBusinessRules));
      
       
       //UseValidationException
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(configuration =>
        {
            //Mediatr
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            //UseValidationBehavior
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>)); 

            //UseTransactionBehavior
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));

            //UseLoggingBehavior
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        //UseLoggerService FileLog or MsSqlLog
       // services.AddSingleton<LoggerServiceBase, FileLogger>();
        services.AddSingleton<LoggerServiceBase, MsSqlLogger>();
        return services;
    }
    //BaseBusinessRules Assembly
    public static IServiceCollection AddSubClassesOfType(
       this IServiceCollection services,
       Assembly assembly,
       Type type,
       Func<IServiceCollection, Type, IServiceCollection> addWithLifeCycle = null
   )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }

}
```    
## Program.cs
```Csharp
   
    builder.Services.AddApplicationServices();
    //LoggerImplement
     builder.Services.AddHttpContextAccessor();

     //Middleware
       if (app.Environment.IsProduction())
            {
                app.ConfigureCustomExceptionMiddleware();
            }
``` 
## appsettings
```Csharp
   "SeriLogConfigurations": {
    "FileLogConfiguration": {
      "FolderPath": "/logs/"
    },
    "MsSqlConfiguration": {
      "AutoCreateSqlTable": true,
      "ConnectionString": "DatabaseConnection",
      "TableName": "Logs"
    }
  },
```

## Command
```Csharp
    //UseTransaction And Logging
   public class CreateExampleCommand() :IRequest<CreatedExampleResponse>, ITransactionalRequest,ILoggableRequest
```
## Handler
```Csharp
    internal sealed class CreateExampleCommandHandler(
        IExampleRepository exampleRepository,
        IMapper mapper,
        ExampleBusinessRules exampleBusinessRules
        ) : IRequestHandler<CreateExampleCommand, CreatedExampleResponse>
    {
   public async Task<CreatedExampleResponse> Handle(CreateExampleCommand request, CancellationToken cancellationToken)
        {
            //Use Business Rules
            await exampleBusinessRules.ExampleNameCannotBeDupplicatedInserted(request.Name);

            Examle example = mapper.Map<Examle>(request);
            example.Id = Guid.NewGuid();


            var result = await exampleRepository.AddAsync(example, cancellationToken);

            CreatedExampleResponse createdExampleResponse = mapper.Map<CreatedExampleResponse>(result);

            return createdExampleResponse;


        }
```

## BusinessException
```Csharp
     public class ExampleBusinessRules(IExampleRepository exampleRepository):BaseBusinessRules
{
    public async Task ExampleNameCannotBeDupplicatedInserted(string name)
    {
        Example example = await exampleRepository.GetAsync(p=>p.Name.ToLower()==name.ToLower());
        if(example is not null )
        {
            throw new BusinessException("Name is Exist");
        }
    }
}
```
## ValidationException
```Csharp
    public class ExampleCommandValidator:AbstractValidator<CreateExampleCommand>
{
    public CreateExampleCommandValidator()
    {
        RuleFor(c=>c.Name).NotEmpty().MinimumLength(2);
    }
}

```

## NugetPackages
```Csharp
    install webapi: serilog, serilog.sinks.mssql, serilog.sinks.file, EntityFrameworkCore.Tools
    add folder path webApi /logs/
    install webapi: AutoMapper, FluentValidationDependencyInjections, MediatR

```
## MsSql Query
```Csharp
   //Add New Query Database

      CREATE TABLE [Logs] (

   [Id] int IDENTITY(1,1) NOT NULL,
   [Message] nvarchar(max) NULL,
   [MessageTemplate] nvarchar(max) NULL,
   [Level] nvarchar(128) NULL,
   [TimeStamp] datetime NOT NULL,
   [Exception] nvarchar(max) NULL,
   [Properties] nvarchar(max) NULL

   CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([Id] ASC)
);
```
 