﻿CREATE PROCEDURE GetFilteredCampaignTransactions
    @CampaignId INT = 0,
    @AgentId INT = 0,
    @FinalId INT = 0
AS
BEGIN
    SELECT
        C.IDCAMPANYA  AS CampaignID,
        C.Nombre AS CampaignName,
        U.IDUSUARIO AS AgentID,
        U.Nombre AS AgentName,
        U.LOGIN AS AgentLogin,
        COUNT(T.idTransaccion) AS TotalTransactions,
        SUM(CASE WHEN T.tCreacion BETWEEN C.tInicial AND C.tFinal THEN 1 ELSE 0 END) AS TotalTransactionsByCampaignId
    FROM
        TRANSACCION T
    INNER JOIN
        USUARIO U ON T.IDUSUARIO = U.IDUSUARIO
    INNER JOIN
        CAMPANYA C ON T.IDCAMPANYA = C.IDCAMPANYA
    WHERE
        ((C.IDCAMPANYA = @CampaignId)
        OR (U.IDUSUARIO = @AgentId)
        OR (T.idFinal = @FinalId))
    GROUP BY
        C.IDCAMPANYA,
        C.Nombre,
        U.IDUSUARIO,
        U.Nombre,
        U.LOGIN;
END;
