$(document).ready(function () {
    popular();
})


function IncluirBeneficiario() {
    //console.log('famoso passou aqui');

    let beneficiario = {
        "Cpf": document.getElementById('CPFBeneficiario').value,
        "Nome": document.getElementById('NomeBeneficiario').value,
        "IdCliente": document.getElementById('Id').value
    };

    $.ajax({
        url: urlIncluirBeneficiariNaLista,
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
                ModalDialog("Sucesso!", "Beneficiário incluído");
                popular();
            }
    });
}


function popular() {

    console.log('aqui')

    $.ajax({
        url: urlIncluirBeneficiario,
        method: "POST",
        data: null,
        error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
        success:
            function (r) {

                const gridContainer = document.getElementById("listaBeneficiarios");
                gridContainer.innerHTML = "";

                let beneficiariosJson = JSON.parse(r);

                for (var i = 0; i < beneficiariosJson.length; i++) {

                    let beneficiario = beneficiariosJson[i];

                    var novoCampo = `
                        <div id="Beneficiario${i}" class="grid-item">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <input required="required" type="text" class="form-control" id="CPFBeneficiario${i}" name="CPFBeneficiario${i}" value="${beneficiario.Cpf}" disabled>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <input required="required" type="text" class="form-control" id="NomeBeneficiario${i}" name="NomeBeneficiario${i}" maxlength="50" value="${beneficiario.Nome}" disabled>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <button id="BtnAlterarBeneficiario${i}" type="button" class="btn btn-primary form-control" onclick="editarBeneficiario('${i}','${beneficiario.Id}')">Alterar</button>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <button id="BtnExcluirBeneficiario${i}" type="button" class="btn btn-primary form-control" onclick="excluirBeneficiario('${beneficiario.Id}','${beneficiario.Cpf}','${beneficiario.Nome}')">Excluir</button>
                                </div>
                            </div>
                        </div> `;

                    $('#listaBeneficiarios').append(novoCampo);
                }
            }
    });
}

function editarBeneficiario(index, beneficarioId) {

    var botaoLabel = $(`#BtnAlterarBeneficiario${index}`).text();

    if (botaoLabel != "Salvar") {

        let cpfBenef = document.getElementById(`CPFBeneficiario${index}`);
        cpfBenef.disabled = false;
        let nomeBenef = document.getElementById(`NomeBeneficiario${index}`);
        nomeBenef.disabled = false;

        $(`#BtnAlterarBeneficiario${index}`).html("Salvar");
    }
    else {

        let cpfBenef = document.getElementById(`CPFBeneficiario${index}`);
        cpfBenef.disabled = true;
        let nomeBenef = document.getElementById(`NomeBeneficiario${index}`);
        nomeBenef.disabled = true;

        $(`#BtnAlterarBeneficiario${index}`).html("Alterar");

        editaBeneficiario(index,beneficarioId);
    }
}

function editaBeneficiario(index,beneficarioId) {
    let cpfEditado = document.getElementById(`CPFBeneficiario${index}`).value;
    let nomeEditado = document.getElementById(`NomeBeneficiario${index}`).value;

    $.ajax({
        url: editarListaBeneficiario,
        method: "POST",
        data: {
            "Cpf": cpfEditado,
            "Nome": nomeEditado,
            "Id": beneficarioId
        },
        error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
        success:
            function (r) {


            }
    });
}

function excluirBeneficiario(beneficarioId, cpfBeneficiario, nomeBeneficiario) {

    $.ajax({
        url: urlExcluirBeneficiario,
        method: "POST",
        data: {
            "Id": beneficarioId,
            "Cpf": cpfBeneficiario,
            "Nome": nomeBeneficiario
        },
        error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
        success:
            function (r) {
                popular();
            }
    });

}