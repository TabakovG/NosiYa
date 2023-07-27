document.addEventListener("DOMContentLoaded", function () {
	var deleteCommentForms = document.querySelectorAll(".delete-comment-form");
	deleteCommentForms.forEach(function (form) {
		form.addEventListener("submit", function (e) {
			// Display a confirmation dialog
			var confirmed = confirm("Сигурни ли сте, че искате да изтриете елемента?");

			// If the user cancels the confirmation, prevent the form submission
			if (!confirmed) {
				e.preventDefault();
			}
		});
	});
});