# Mrx.ApiClientGenerator CLI Usage

This app now supports two modes:

- No args: starts WinForms UI.
- With args: runs in CLI mode (no UI).

## 1) Run from profile file

```powershell
Mrx.ApiClientGenerator.exe --profile-file "D:\path\profiles.json" --profile "profile 1"
```

Or by index:

```powershell
Mrx.ApiClientGenerator.exe --profile-file "D:\path\profiles.json" --profile-index 0
```

## 2) Run with direct args

```powershell
Mrx.ApiClientGenerator.exe `
  --url "https://localhost:5001/swagger/v1/swagger.json" `
  --generate-path "D:\out\Api.ts" `
  --language TypeScript `
  --base-url "https://localhost:5001" `
  --name "my-profile" `
  --ts-datetime-type Date
```

Supported `--language` values:

- `TypeScript`
- `CSharp`
- `Dart`

## Exit Codes

- `0` success
- `1` input/argument error
- `2` generation/runtime error

## Help

```powershell
Mrx.ApiClientGenerator.exe --help
```
