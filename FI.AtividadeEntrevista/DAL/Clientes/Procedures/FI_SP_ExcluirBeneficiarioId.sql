﻿CREATE PROCEDURE FI_SP_ExcluirBeneficiarioId
    @Id BIGINT
AS
BEGIN
    DELETE
    BENEFICIARIOS
    WHERE ID = @Id;
END;