$(document).ready(function () {

})


function IncluirBeneficiario() {
    //console.log('famoso passou aqui');

    let beneficiario = {
        "Cpf": document.getElementById('CPFBeneficiario').value,
        "Nome": document.getElementById('NomeBeneficiario').value,
        "IdCliente": document.getElementById('Id').value
    };

    $.ajax({
        url: urlIncluirBeneficiario,
        method: "POST",
        data: beneficiario,
        error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
        success:
            function (r) {
                ModalDialog("Sucesso!", r);
            }
    });
}