// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function EnterSite() {
    var x = document.getElementById("name-box").value;
    x = x.replace(/</g, "_").replace(/>/g, "_");

    navigator.sendBeacon("/name", JSON.stringify({"name": x}));

    document.getElementById("nameBox").style.display = "none";

}