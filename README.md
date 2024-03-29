## Initial Setup
1. Ensure that your hardware meets the requirements as specified in the documentation of [Stable Diffusion Web UI](https://github.com/AUTOMATIC1111/stable-diffusion-webui)  
2. Get Stable Diffusion Web UI locally, either by doing `git clone` or downloading the latest version directly from the [Releases page](https://github.com/AUTOMATIC1111/stable-diffusion-webui/releases).
3. Modify the execution script to enable API calls.  
   On Windows for example, modify `webui-user.bat` and edit the line containing `COMMANDLINE_ARGS` to:  
`set COMMANDLINE_ARGS=--api --api-log`  
4. Execute the execution script, i.e `webui-user.bat` on Windows.
5. Confirm that Stable Diffusion Web UI is running, for example by visiting http://127.0.0.1:7860.
