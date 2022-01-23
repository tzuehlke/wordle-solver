# About

This app helps to solve the daily [Wordle](https://www.powerlanguage.co.uk/wordle/) puzzle (a word guessing game).

A description of the app is comming soon in a blogpost.

You can use it on [wordle.zuehlke.cloud](https://wordle.zuehlke.cloud).

![Overview picture](https://user-images.githubusercontent.com/32843441/150650516-1998923f-b836-4256-99b0-a5e3e86a3322.png)


# How to use
The dictionary will be searched in 3 steps. All steps are optional.
1. Step: use the unsued letters for the "Possible Characters" field. The dictionary will be filtert for words, that only conatains this characters.
1. Step: use the green and yellow letters in "Must Characters". The wordlist from step 1 will be filtert for words, that contains all the letters in this field only.
1. Step: if you have positonal information of the letters, you can write them as [regular expression](https://cheatography.com/davechild/cheat-sheets/regular-expressions/). You can write `.....` for no positional definition. If you have a green letter, you should write the letter at the correct position (e.g. if you have an E at second position, you can write `.E...`). If you have an yellow letter, it is at the wrong spot but the word contains it at least once and you should write a "not" at the position in regex (e.g. if an A is not correct at the third position, you can write `..[^A]..`).

|Wordle|Solver|
|---|---|
|![example 1](https://user-images.githubusercontent.com/32843441/150654786-98310aad-bf1a-49a8-bd72-0eba7554241c.png)|![example 1 solution](https://user-images.githubusercontent.com/32843441/150654815-3793c789-d3d1-4f55-b8c7-d32d4b0adb63.png)|
|![example 2](https://user-images.githubusercontent.com/32843441/150654867-2984459c-06c9-4591-ac5d-f434fd0f80e3.png)|![example 2 solution](https://user-images.githubusercontent.com/32843441/150654884-0ec86a01-9769-48cc-9b65-8ed295e27814.png)|
|![example 3](https://user-images.githubusercontent.com/32843441/150654910-234be142-33a8-4624-a890-276f7ebea979.png)|![example 3 solution](https://user-images.githubusercontent.com/32843441/150654957-d636daec-21b3-4e07-9a26-b1798d2d4ec1.png)|


# Build & Deploy
The solution using the remote container extention of Visual Studio Code.
1. Build and test
    ```
    dotnet build
    dotnet watch run
    ```
1. publish
    ```
    dotnet publish
    ```
1. copy to Azure storage account with AzCopy
   * AzCopy is installed at `/opt/azcopy`
   * activate the static web feature at your storage account
   * get a SAS Token of the storage account and the storage account URL in the `copytostorage.sh` file
   * ```
     TARGET='https://<YOURSTORAGE ACCOUNT>.blob.core.windows.net/$web'
     SAS='?sv=2020-08-04...'
     ```
   * run the copy bash script `./copytostorage.sh`