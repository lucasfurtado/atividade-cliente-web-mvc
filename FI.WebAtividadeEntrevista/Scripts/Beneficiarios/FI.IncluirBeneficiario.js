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
                        <div id="Beneficiario${i}" class="grid-item" style="background-color: #F5F5F5;">
                            <div class="col-md-4 mx-auto">
                                <div class="form-group">
                                    <input class="form-control GridCPFBeneficiario CPF" required="required" type="text" id="CPFBeneficiario${i}" name="CPFBeneficiario${i}" placeholder="Ex.: 010.011.111-00" style="border: none; background-color: #F5F5F5; color: black; margin-top: 5px " value="${beneficiario.Cpf}" disabled>
                                </div>
                            </div>
                            <div class="col-md-4 mx-auto">
                                <div class="form-group">
                                    <input required="required" type="text" class="form-control GridNomeBeneficiario" id="NomeBeneficiario${i}" name="NomeBeneficiario${i}" placeholder="Ex.: João da Silva" maxlength="50" style="border: none; background-color: #F5F5F5; color: black;margin-top:5px" value="${beneficiario.Nome}" disabled>
                                </div>
                            </div>
                            <div class="col-md-2 mx-auto">
                                <div class="form-group">
                                    <button id="AlterarBeneficiario${i}" type="button" class="btn btn-sm btn-primary form-control" style="margin-top:5px" onclick="editarBeneficiario('${i}')">Alterar</button>
                                </div>
                            </div>
                            <div class="col-md-2 mx-auto">
                                <div class="form-group">
                                    <button id="ExcluirBeneficiario${i}" type="button" class="btn btn-sm btn-danger form-control" style="margin-top:5px" onclick="ExcluirBeneficiario('${i}')">Excluir</button>
                                </div>
                            </div>
                        </div> `;

                    $('#listaBeneficiarios').append(novoCampo);
                }
                //ModalDialog("Sucesso","Inserido");
            }
    });

    //let beneficiarios = document.getElementById('Beneficiarios').value;
    //let beneficiariosJson = JSON.parse(beneficiarios);

    //for (var i = 0; i < beneficiariosJson.length; i++) {

    //    let beneficiario = beneficiariosJson[i];

    //    var novoCampo = `
    //    <div id="Beneficiario${i}" class="grid-item" style="background-color: #F5F5F5;">
    //        <div class="col-md-4 mx-auto">
    //            <div class="form-group">
    //                <input class="form-control GridCPFBeneficiario CPF" required="required" type="text" id="CPFBeneficiario${i}" name="CPFBeneficiario${i}" placeholder="Ex.: 010.011.111-00" style="border: none; background-color: #F5F5F5; color: black; margin-top: 5px " value="${beneficiario.Cpf}" disabled>
    //            </div>
    //        </div>
    //        <div class="col-md-4 mx-auto">
    //            <div class="form-group">
    //                <input required="required" type="text" class="form-control GridNomeBeneficiario" id="NomeBeneficiario${i}" name="NomeBeneficiario${i}" placeholder="Ex.: João da Silva" maxlength="50" style="border: none; background-color: #F5F5F5; color: black;margin-top:5px" value="${beneficiario.Nome}" disabled>
    //            </div>
    //        </div>
    //        <div class="col-md-2 mx-auto">
    //            <div class="form-group">
    //                <button id="AlterarBeneficiario${i}" type="button" class="btn btn-sm btn-primary form-control" style="margin-top:5px" onclick="editarBeneficiario('${i}')">Alterar</button>
    //            </div>
    //        </div>
    //        <div class="col-md-2 mx-auto">
    //            <div class="form-group">
    //                <button id="ExcluirBeneficiario${i}" type="button" class="btn btn-sm btn-danger form-control" style="margin-top:5px" onclick="ExcluirBeneficiario('${i}')">Excluir</button>
    //            </div>
    //        </div>
    //    </div> `;

    //    $('#listaBeneficiarios').append(novoCampo);
    //}
    
}