CREATE PROCEDURE FI_SP_PesqListaBeneficiarios
    @clienteId BIGINT
AS
BEGIN
    SELECT id, nome, cpf, IdCliente
    FROM BENEFICIARIOS
    WHERE IdCliente = @clienteId;
END;