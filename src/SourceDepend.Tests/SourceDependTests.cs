using Xunit.Abstractions;

namespace SourceDepend.Tests;

public class SourceDependTests
{
    private readonly ITestOutputHelper _output;

    public SourceDependTests(ITestOutputHelper output) => this._output = output ?? throw new ArgumentNullException(nameof(output));

    [Fact]
    public void SourceDepend_WithoutNamespace_ShouldNotWriteNamespace()
    {
        //Arrange
        var source = """
                     public partial class ExampleService
                     {

                         [Dependency]
                         private readonly IAnotherService anotherService;
                     }

                     public interface IAnotherService {}
                     """;

        var expec = """
                     // <auto-generated/>
                     #pragma warning disable
                     #nullable enable
                     /// <inheritdoc/>
                     public partial class ExampleService
                     {
                         public ExampleService(IAnotherService anotherService)
                         {

                     #if NET6_0_OR_GREATER
                             ArgumentNullException.ThrowIfNull(anotherService);
                     #endif
                             PreConstruct();

                             this.anotherService = anotherService;

                             PostConstruct();
                         }
                     
                         partial void PreConstruct();
                         partial void PostConstruct();
                     }

                     """;
        //Act
        var actual = SourceCompiler.GetGeneratedOutput(source, _output);

        //Assert
        Assert.Equal(expec, actual.Item2);
    }

    [Fact]
    public void SourceDepend_WithPublic_ShouldRepeateThis()
    {
        //Arrange
        var source = """
                     namespace ConsoleApp;

                     public partial class ExampleService
                     {

                         [Dependency]
                         private readonly IAnotherService anotherService;
                     }

                     public interface IAnotherService {}
                     """;

        var expec = """
                     // <auto-generated/>
                     #pragma warning disable
                     #nullable enable
                     namespace ConsoleApp
                     {
                         /// <inheritdoc/>
                         public partial class ExampleService
                         {
                             public ExampleService(ConsoleApp.IAnotherService anotherService)
                             {

                     #if NET6_0_OR_GREATER
                                 ArgumentNullException.ThrowIfNull(anotherService);
                     #endif
                                 PreConstruct();

                                 this.anotherService = anotherService;

                                 PostConstruct();
                             }

                             partial void PreConstruct();
                             partial void PostConstruct();
                         }
                     }

                     """;
        //Act
        var actual = SourceCompiler.GetGeneratedOutput(source, _output);

        //Assert
        Assert.Equal(expec, actual.Item2);
    }

    [Fact]
    public void SourceDepend_WithInternal_ShouldRepeateThis()
    {
        //Arrange
        var source = """
                     namespace ConsoleApp;

                     internal partial class ExampleService
                     {

                         [Dependency]
                         private readonly IAnotherService anotherService;
                     }

                     public interface IAnotherService {}
                     """;

        var expec = """
                     // <auto-generated/>
                     #pragma warning disable
                     #nullable enable
                     namespace ConsoleApp
                     {
                         /// <inheritdoc/>
                         internal partial class ExampleService
                         {
                             public ExampleService(ConsoleApp.IAnotherService anotherService)
                             {

                     #if NET6_0_OR_GREATER
                                 ArgumentNullException.ThrowIfNull(anotherService);
                     #endif
                                 PreConstruct();

                                 this.anotherService = anotherService;

                                 PostConstruct();
                             }

                             partial void PreConstruct();
                             partial void PostConstruct();
                         }
                     }

                     """;
        //Act
        var actual = SourceCompiler.GetGeneratedOutput(source, _output);

        //Assert
        Assert.Equal(expec, actual.Item2);
    }

    [Fact]
    public void SourceDepend_WithInternalSealed_ShouldRepeateThis()
    {
        //Arrange
        var source = """
                     namespace ConsoleApp;

                     internal sealed partial class ExampleService
                     {

                         [Dependency]
                         private readonly IAnotherService anotherService;
                     }

                     public interface IAnotherService {}
                     """;

        var expec = """
                     // <auto-generated/>
                     #pragma warning disable
                     #nullable enable
                     namespace ConsoleApp
                     {
                         /// <inheritdoc/>
                         internal sealed partial class ExampleService
                         {
                             public ExampleService(ConsoleApp.IAnotherService anotherService)
                             {

                     #if NET6_0_OR_GREATER
                                 ArgumentNullException.ThrowIfNull(anotherService);
                     #endif
                                 PreConstruct();

                                 this.anotherService = anotherService;

                                 PostConstruct();
                             }

                             partial void PreConstruct();
                             partial void PostConstruct();
                         }
                     }

                     """;
        //Act
        var actual = SourceCompiler.GetGeneratedOutput(source, _output);

        //Assert
        Assert.Equal(expec, actual.Item2);
    }

    [Fact]
    public void SourceDepend_WithInternalAbstract_ShouldRepeateThis()
    {
        //Arrange
        var source = """
                     namespace ConsoleApp;

                     internal abstract partial class ExampleService
                     {

                         [Dependency]
                         private readonly IAnotherService anotherService;
                     }

                     public interface IAnotherService {}
                     """;

        var expec = """
                     // <auto-generated/>
                     #pragma warning disable
                     #nullable enable
                     namespace ConsoleApp
                     {
                         /// <inheritdoc/>
                         internal abstract partial class ExampleService
                         {
                             public ExampleService(ConsoleApp.IAnotherService anotherService)
                             {

                     #if NET6_0_OR_GREATER
                                 ArgumentNullException.ThrowIfNull(anotherService);
                     #endif
                                 PreConstruct();

                                 this.anotherService = anotherService;

                                 PostConstruct();
                             }

                             partial void PreConstruct();
                             partial void PostConstruct();
                         }
                     }

                     """;
        //Act
        var actual = SourceCompiler.GetGeneratedOutput(source, _output);

        //Assert
        Assert.Equal(expec, actual.Item2);
    }

    [Fact]
    public void SourceDepend_WithGenerics_ShouldRepeatTheseGenerics()
    {
        //Arrange
        var source = """
                     public partial class ExampleService<TKey,TValue>
                     {

                         [Dependency]
                         private readonly IAnotherService anotherService;
                     }

                     public interface IAnotherService {}
                     """;

        var expec = """
                     // <auto-generated/>
                     #pragma warning disable
                     #nullable enable
                     /// <inheritdoc/>
                     public partial class ExampleService<TKey,TValue>
                     {
                         public ExampleService(IAnotherService anotherService)
                         {

                     #if NET6_0_OR_GREATER
                             ArgumentNullException.ThrowIfNull(anotherService);
                     #endif
                             PreConstruct();

                             this.anotherService = anotherService;

                             PostConstruct();
                         }
                     
                         partial void PreConstruct();
                         partial void PostConstruct();
                     }

                     """;
        //Act
        var actual = SourceCompiler.GetGeneratedOutput(source, _output);

        //Assert
        Assert.Equal(expec, actual.Item2);
    }

    [Fact]
    public void SourceDepend_WithGeneric_ShouldRepeatTheseGenerics()
    {
        //Arrange
        var source = """
                     public partial class ExampleService<T>
                     {

                         [Dependency]
                         private readonly IAnotherService anotherService;
                     }

                     public interface IAnotherService {}
                     """;

        var expec = """
                     // <auto-generated/>
                     #pragma warning disable
                     #nullable enable
                     /// <inheritdoc/>
                     public partial class ExampleService<T>
                     {
                         public ExampleService(IAnotherService anotherService)
                         {

                     #if NET6_0_OR_GREATER
                             ArgumentNullException.ThrowIfNull(anotherService);
                     #endif
                             PreConstruct();

                             this.anotherService = anotherService;

                             PostConstruct();
                         }
                     
                         partial void PreConstruct();
                         partial void PostConstruct();
                     }

                     """;
        //Act
        var actual = SourceCompiler.GetGeneratedOutput(source, _output);

        //Assert
        Assert.Equal(expec, actual.Item2);
    }

    [Fact]
    public void SourceDepend_WithBaseWithConstructorWithParameters_ShouldRepeatAndCallThisBaseConstructor()
    {
        //Arrange
        var source = """
                     public partial class ExampleService : BaseService
                     {

                         [Dependency]
                         private readonly IAnotherService anotherService;
                     }

                     public partial class BaseService
                     {
                         private readonly string _someString;
                         public BaseService(string someString)
                         {
                            _someString = someString;
                         }
                     }

                     public interface IAnotherService {}
                     """;

        var expec = """
                     // <auto-generated/>
                     #pragma warning disable
                     #nullable enable
                     /// <inheritdoc/>
                     public partial class ExampleService
                     {
                         public ExampleService(IAnotherService anotherService, string someString) : base(someString)
                         {

                     #if NET6_0_OR_GREATER
                             ArgumentNullException.ThrowIfNull(anotherService);
                     #endif
                             PreConstruct();

                             this.anotherService = anotherService;

                             PostConstruct();
                         }
                     
                         partial void PreConstruct();
                         partial void PostConstruct();
                     }

                     """;
        //Act
        var actual = SourceCompiler.GetGeneratedOutput(source, _output);

        //Assert
        Assert.Equal(expec, actual.Item2);
    }

    [Fact]
    public void SourceDepend_WithBaseWithDependencyAttributes_ShouldRepeatTheseAndCallThisBaseConstructor()
    {
        //Arrange
        var source = """
                     public partial class ExampleService : BaseService
                     {

                         [Dependency]
                         private readonly IAnotherService anotherService;
                     }

                     public partial class BaseService
                     {
                         [Dependency]
                         private readonly string _someString;
                     }

                     public interface IAnotherService {}
                     """;

        var expec = """
                     // <auto-generated/>
                     #pragma warning disable
                     #nullable enable
                     /// <inheritdoc/>
                     public partial class ExampleService
                     {
                         public ExampleService(IAnotherService anotherService, string someString) : base(someString)
                         {

                     #if NET6_0_OR_GREATER
                             ArgumentNullException.ThrowIfNull(anotherService);
                     #endif
                             PreConstruct();

                             this.anotherService = anotherService;

                             PostConstruct();
                         }
                     
                         partial void PreConstruct();
                         partial void PostConstruct();
                     }

                     """;
        //Act
        var actual = SourceCompiler.GetGeneratedOutput(source, _output);

        //Assert
        Assert.Equal(expec, actual.Item1);
    }
}
