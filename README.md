# Project 1024
1024 Web 短视频  
第二届七牛云 1024 创作节参赛项目

## 技术栈
- [.NET](https://dot.net) / [C#](https://dotnet.microsoft.com/zh-cn/languages/csharp)
- [ASP.NET Core](https://dotnet.microsoft.com/zh-cn/apps/aspnet) / [Web API](https://dotnet.microsoft.com/zh-cn/apps/aspnet/apis)
- [Entity Framework Core](https://learn.microsoft.com/zh-cn/ef/)
- [Blazor WebAssembly](https://dotnet.microsoft.com/zh-cn/apps/aspnet/web-apps/blazor)
- [MASA Blazor](https://masastack.com/blazor)
- [Swiper](https://swiperjs.com/)
- [Qiniu (Cloud) C# SDK](https://github.com/qiniu/csharp-sdk)

## 项目结构
该解决方案包含三个项目：  

- Project1024.Server
- Project1024.Wasm
- Project1024.Shared

受益于前/后端使用同一种语言开发，我们可以在项目间共享一些代码。  
这些代码被剥离到一个单独的项目中，即 `Project1024.Shared`。

```
.
├── Project1024.Server  -- 服务端项目
│   ├── Controllers         -- Web API 控制器
│   ├── Data                -- EF Core 数据库上下文
│   ├── Migrations          -- EF Core 数据库迁移
│   ├── Models              -- 数据库实体模型
│   ├── Properties          -- 运行配置（监听地址等）
│   ├── Services            -- 服务
│   └── Settings            -- 配置实体类（JWT）
├── Project1024.Shared  -- 服务端/客户端共享代码项目
│   ├── Models              -- 共享模型（DTO）
│   └── Services            -- 共享服务（接口）
└── Project1024.Wasm    -- 客户端（前端）项目
    ├── Components          -- 封装组件
    ├── Pages               -- 页面
    ├── Properties          -- 运行配置（监听地址等）
    ├── Services            -- 服务
    ├── Shared              -- 共享组件（页面布局）
    └── wwwroot             -- 静态资源
```

## 构建/运行说明
- 构建项目前请确保安装 .NET 7.0 SDK
- 在解决方案（.sln）文件所在目录执行 `dotnet workload restore` 以安装项目所需的工作负载
- 执行 `dotnet run --project Project1024.Server` 以构建并运行服务端
- 在浏览器中打开控制台输出的 URL 链接以访问应用

## DEMO
待补充
