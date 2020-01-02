# FixPackageVersions
Fixes PackageReference entries in projects to use the compact form for versions

Sometimes Visual Studio splits the `<PackageReference>` entries in this way:
```
    <PackageReference Include="PackageName">
    <Version>1.2.3</Version>
    </PackageReference>
```
And other times it uses the neater
```
    <PackageReference Include="PackageName" Version="1.2.3" />
```
This utility cleans up an XML fragment into the neater version.
