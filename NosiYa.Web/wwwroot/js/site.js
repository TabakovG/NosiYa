// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function checkTotalSize(input) {
    const maxSizeBytes = 15 * 1024 * 1024; // 15 MB

    const files = input.files;
    let totalSize = 0;

    for (let i = 0; i < files.length; i++) {
        totalSize += files[i].size;
    }

    if (totalSize > maxSizeBytes) {
        alert("Total file size exceeds the allowed limit of 15 MB.");
        input.value = ""; // Clear the selected files
    }
}
