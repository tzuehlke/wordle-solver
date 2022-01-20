#!/bin/bash

# set execute permissions first: sudo chmod +x copytostorage.sh

TARGET='https://<YOURSTORAGE ACCOUNT>.blob.core.windows.net/$web'
SAS='?sv=2020-08-04...'
echo "copy to $TARGET"

sudo /opt/azcopy/azcopy copy '/workspaces/wordle-solver/bin/Debug/net6.0/publish/wwwroot/*' "$TARGET$SAS" --recursive=true