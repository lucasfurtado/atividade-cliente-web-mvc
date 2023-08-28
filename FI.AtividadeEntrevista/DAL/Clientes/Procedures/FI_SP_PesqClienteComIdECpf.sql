CREATE PROCEDURE FI_SP_PesqClienteComIdECpf
    @clienteId BIGINT,
	@clienteCpf varchar(14)
AS
BEGIN
    SELECT *
    FROM CLIENTES
    WHERE ID = @clienteId AND CPF = @clienteCpf;
END;