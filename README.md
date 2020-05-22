### Simple Project to reproduce libgdiplus limitation issue when using aspose on PDF to Image conversion
`** (process:47751): WARNING **: 13:41:11.477: Path conversion requested 33204137848336512 bytes (515396064 x 515396064). Maximum size is 1073741824 bytes.`

error causes crashing application. patches to bypass limits was to fork out libgdiplus repository and hardcode limits. testfile in particular still causes issues.
Testfile is under `TestFiles` folder.

### Running Locally
- supply aspose license under appsettings.json
- execute run.sh

### References:
https://forum.aspose.com/t/pdf-to-image-dotnetcore-2-1-error-region-cgdipcombineregionpath-assertion-failed-region-bitmap/178516/16?u=ivanperever
https://github.com/jesse-myob/libgdiplus