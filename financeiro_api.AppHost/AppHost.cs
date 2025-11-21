var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("financeiro");

var api = builder.AddProject<Projects.myapi>("api")
       .WithExternalHttpEndpoints();


builder.AddNpmApp("angular", "../financeiro_zurd")
   .WithReference(api)
   .WaitFor(api)
   .WithHttpEndpoint(env: "PORT")
   .WithExternalHttpEndpoints()
   .PublishAsDockerFile();

builder.Build().Run();
