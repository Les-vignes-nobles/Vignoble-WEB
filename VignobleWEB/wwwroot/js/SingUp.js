$(function () {
	$.validator.setDefaults({
		highlight: function (element) {
			$(element)
				.closest('.form-group')
				.addClass('has-error')
		},
		unhighlight: function (element) {
			$(element)
				.closest('.form-group')
				.removeClass('has-error')
		}
	});

	$.validate({
		rules:
		{
			password: "required",
			birthDate: "required",
			weight: {
				required: true,
				number: true
			},
			height: {
				required: true,
				number: true
			},
			email: {
				required: true,
				email: true
			}
		},
		messages: {
			email: {
				required: true,
				email: true
			}
		},
		password: {
			required: " Veuillez entrer un mot de passe"
		},
		birthDate: {
			required: " Veuillez entrer une date d'anniversaire"
		},
		email: {
			required: ' Veuillez entrer une adresse mail',
			email: ' Veuillez entrer une adresse mail valide'
		},
		weight: {
			required: " Please enter your weight",
			number: " Only numbers allowed"
		},
		height: {
			required: " Please enter your height",
			number: " Only numbers allowed"
		},
	}
			
	});
});