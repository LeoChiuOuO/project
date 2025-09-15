// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showAlert(message, type) {
    const html = `<div class="alert alert-${type} alert-dismissible fade show" role="alert">
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </div>`;
    const container = document.getElementById("alert-container");
    if (container) {
        container.innerHTML = html;
        setTimeout(() => {
            const alert = bootstrap.Alert.getOrCreateInstance(container.querySelector('.alert'));
            alert.close();
        }, 3000);
    }
}
