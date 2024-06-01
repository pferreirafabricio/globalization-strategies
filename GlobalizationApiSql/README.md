# 🚶‍♂️ Installing and Running

1. Run `dotnet restore`
2. Guarantee that you have Entity Framework Core installed globally `dotnet tool install --global dotnet-ef`

> If you are using Linux, maybe you will need to add the dotnet-ef to your path.
> Like this:
>
> ```bash
> sudo nano .bashrc # or sudo nano .zshrc
> # Append this to the bottom of the file
> export PATH="$PATH:$HOME/.dotnet/tools/"
> ```

3. If this is your first time running the project execute `./migration-helper.ps1` and select the option `3. Update Database`

> If you are using Linux, you can run the script using the PowerShell Core. [Install PowerShell on Linux](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-linux?view=powershell-7.4)

4. Run the API with

```bash
dotnet run
```
