BEGIN
    SELECT
        C.IDCAMPANYA AS CampaignID,
        C.Nombre AS CampaignName,
        U.IDUSUARIO AS AgentID,
        U.Nombre AS AgentName,
        U.LOGIN AS AgentLogin,
        COUNT(T.idTransaccion) AS TotalTransactions,
        SUM(DATEDIFF(MINUTE, C.tInicio, C.tFinal)) AS TotalTimeInMinutes
    FROM
        TRANSACCION T
    INNER JOIN
        USUARIO U ON T.IDAGENTE = U.IDUSUARIO
    INNER JOIN
        CAMPANYA C ON T.IDCAMPANYA = C.IDCAMPANYA
    GROUP BY
        C.IDCAMPANYA,
        C.Nombre,
        U.IDUSUARIO,
        U.Nombre,
        U.LOGIN
END;