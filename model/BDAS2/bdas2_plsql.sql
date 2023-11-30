-- Procedure to add new Klient
CREATE OR REPLACE PROCEDURE AddNewKlient(
    p_id_klient IN NUMBER,
    p_cislo_prukazu IN NUMBER,
    p_jmeno IN VARCHAR2,
    p_prijmeni IN VARCHAR2,
    p_klient_email IN VARCHAR2,
    p_adresa_id_adres IN NUMBER,
    p_bank_id_bank IN NUMBER,
    p_telefoni_cislo IN VARCHAR2,
    p_zame_id_zamestnanec IN NUMBER,
    p_odesi_file_id_file IN NUMBER,
    p_odes_file_id_klient IN NUMBER,
    p_id_file1 IN NUMBER,
    p_id_klient2 IN NUMBER
) AS
BEGIN
    INSERT INTO klient (
        id_klient,
        cislo_prukazu,
        jmeno,
        prijmeni,
        klient_email,
        adresa_id_adres,
        bank_id_bank,
        telefoni_cislo,
        zame_id_zamestnanec,
        odesi_file_id_file,
        odes_file_id_klient,
        id_file1,
        id_klient2
    ) VALUES (
        p_id_klient,
        p_cislo_prukazu,
        p_jmeno,
        p_prijmeni,
        p_klient_email,
        p_adresa_id_adres,
        p_bank_id_bank,
        p_telefoni_cislo,
        p_zame_id_zamestnanec,
        p_odesi_file_id_file,
        p_odes_file_id_klient,
        p_id_file1,
        p_id_klient2
    );

    -- Log the change
    LogPackage.LogChange('INSERT', 'klient', NULL, NULL, NULL, USER);
    
    COMMIT;
END AddNewKlient;
/

CREATE OR REPLACE PROCEDURE AddNewZamestnanec(
    p_id_zamestnanec IN NUMBER,
    p_jmeno IN VARCHAR2,
    p_prijmeni IN VARCHAR2,
    p_mzda IN NUMBER,
    p_telefoni_cislo IN VARCHAR2,
    p_adresa_id_adres IN NUMBER,
    p_bank_id_bank IN NUMBER,
    p_status_id_status IN NUMBER,
    p_zame_id_zamestnanec IN NUMBER,
    p_pobocka_id_pobocka IN NUMBER,
    p_prace_pozice_id_pozice IN NUMBER,
    p_email_zamestnanec IN VARCHAR2,
    p_id_file1 IN NUMBER,
    p_id_klient1 IN NUMBER
) AS
BEGIN
    INSERT INTO zamestnanec (
        id_zamestnanec,
        jmeno,
        prijmeni,
        mzda,
        telefoni_cislo,
        adresa_id_adres,
        bank_id_bank,
        status_id_status,
        zamestnanec_id_zamestnanec1,
        pobocka_id_pobocka,
        prace_pozice_id_pozice,
        email_zamestnanec
    ) VALUES (
        p_id_zamestnanec,
        p_jmeno,
        p_prijmeni,
        p_mzda,
        p_telefoni_cislo,
        p_adresa_id_adres,
        p_bank_id_bank,
        p_status_id_status,
        p_zame_id_zamestnanec,
        p_pobocka_id_pobocka,
        p_prace_pozice_id_pozice,
        p_email_zamestnanec
    );

    -- Log the change
    LogPackage.LogChange('INSERT', 'zamestnanec', NULL, NULL, NULL, USER);
    
    COMMIT;
END AddNewZamestnanec;
/

CREATE OR REPLACE PROCEDURE RemoveKlient(
    p_id_klient IN NUMBER
) AS
BEGIN
    -- Log the change before deletion
    LogPackage.LogChange('DELETE', 'klient', NULL, NULL, NULL, USER);

    -- Delete the Klient
    DELETE FROM klient WHERE id_klient = p_id_klient;

    COMMIT;
END RemoveKlient;
/

CREATE OR REPLACE PROCEDURE RemoveZamestnanec(
    p_id_zamestnanec IN NUMBER
) AS
BEGIN
    -- Log the change before deletion
    LogPackage.LogChange('DELETE', 'zamestnanec', NULL, NULL, NULL, USER);

    -- Delete the Zamestnanec
    DELETE FROM zamestnanec WHERE id_zamestnanec = p_id_zamestnanec;

    COMMIT;
END RemoveZamestnanec;
/

CREATE OR REPLACE PROCEDURE EditKlient(
    p_id_klient IN NUMBER,
    p_new_email IN VARCHAR2,
    p_new_telefoni_cislo IN VARCHAR2,
    p_new_name IN VARCHAR2,
    p_new_surname IN VARCHAR2,
    p_role IN VARCHAR2,
    p_new_ulice IN VARCHAR2,
    p_new_mesto IN VARCHAR2,
    p_new_cislo_popisne IN VARCHAR2,
    p_new_psc IN CHAR,
    p_new_stat IN VARCHAR2,
    p_new_cislo_prukazu IN NUMBER
) AS
BEGIN
    IF p_new_email IS NOT NULL THEN
        LogPackage.LogChange('UPDATE', 'klient', 'klient_email', NULL, p_new_email, USER);
    END IF;
    IF p_new_telefoni_cislo IS NOT NULL THEN
        LogPackage.LogChange('UPDATE', 'klient', 'telefoni_cislo', NULL, p_new_telefoni_cislo, USER);
    END IF;
    IF p_new_name IS NOT NULL THEN
        LogPackage.LogChange('UPDATE', 'klient', 'jmeno', NULL, p_new_name, USER);
    END IF;
    IF p_new_surname IS NOT NULL THEN
        LogPackage.LogChange('UPDATE', 'klient', 'prijmeni', NULL, p_new_surname, USER);
    END IF;

    IF p_role = 'SELF' THEN
        UPDATE klient k
        SET k.klient_email = COALESCE(p_new_email, k.klient_email),
            k.telefoni_cislo = COALESCE(p_new_telefoni_cislo, k.telefoni_cislo)
        WHERE k.id_klient = p_id_klient;

          EditKlientAddress(
            p_id_klient,
            p_new_ulice,
            p_new_mesto,
            p_new_cislo_popisne,
            p_new_psc,
            p_new_stat
        );
    ELSIF p_role IN ('ZAMESTNANEC', 'ADMIN') THEN
        UPDATE klient k
        SET k.klient_email = COALESCE(p_new_email, k.klient_email),
            k.telefoni_cislo = COALESCE(p_new_telefoni_cislo, k.telefoni_cislo),
            k.jmeno = COALESCE(p_new_name, k.jmeno),
            k.prijmeni = COALESCE(p_new_surname, k.prijmeni),
            k.cislo_prukazu = COALESCE(p_new_cislo_prukazu, k.cislo_prukazu)
        WHERE k.id_klient = p_id_klient;

          EditKlientAddress(
            p_id_klient,
            p_new_ulice,
            p_new_mesto,
            p_new_cislo_popisne,
            p_new_psc,
            p_new_stat
        );
    ELSE
        raise_application_error(-20223, 'Invalid role specified');
    END IF;

    COMMIT;
END EditKlient;
/


CREATE OR REPLACE PROCEDURE EditKlientAddress(
    p_id_klient IN NUMBER,
    p_new_ulice IN VARCHAR2,
    p_new_mesto IN VARCHAR2,
    p_new_cislo_popisne IN VARCHAR2,
    p_new_psc IN CHAR,
    p_new_stat IN VARCHAR2
) AS
BEGIN
    -- Log the address change before update
    LogPackage.LogChange('UPDATE', 'adresa', 'ulice', NULL, p_new_ulice, USER);
    LogPackage.LogChange('UPDATE', 'adresa', 'mesto', NULL, p_new_mesto, USER);
    LogPackage.LogChange('UPDATE', 'adresa', 'cislo_popisne', NULL, p_new_cislo_popisne, USER);
    LogPackage.LogChange('UPDATE', 'adresa', 'psc', NULL, p_new_psc, USER);
    LogPackage.LogChange('UPDATE', 'adresa', 'stat', NULL, p_new_stat, USER);

    -- Update the address
    UPDATE adresa a
    SET ulice = COALESCE(p_new_ulice, a.ulice),
        mesto = COALESCE(p_new_mesto, a.mesto),
        cislo_popisne = COALESCE(p_new_cislo_popisne, a.cislo_popisne),
        psc = COALESCE(p_new_psc, a.psc),
        stat = COALESCE(p_new_stat, a.stat)
    WHERE a.id_adres = (SELECT adresa_id_adres FROM klient WHERE id_klient = p_id_klient);

    COMMIT;
END EditKlientAddress;
/

CREATE OR REPLACE PROCEDURE EditZamestnanec(
    p_id_zamestnanec IN NUMBER,
    p_new_email IN VARCHAR2,
    p_new_telefoni_cislo IN VARCHAR2,
    p_new_name IN VARCHAR2,
    p_new_surname IN VARCHAR2,
    p_role IN VARCHAR2,
    p_new_mzda IN NUMBER,
    p_new_id_prace_pozice IN NUMBER
) AS
BEGIN
    IF p_new_email IS NOT NULL THEN
        LogPackage.LogChange('UPDATE', 'zamestnanec', 'zamestnanec_email', NULL, p_new_email, USER);
    END IF;
    IF p_new_telefoni_cislo IS NOT NULL THEN
        LogPackage.LogChange('UPDATE', 'zamestnanec', 'telefoni_cislo', NULL, p_new_telefoni_cislo, USER);
    END IF;
    IF p_new_name IS NOT NULL THEN
        LogPackage.LogChange('UPDATE', 'zamestnanec', 'jmeno', NULL, p_new_name, USER);
    END IF;
    IF p_new_surname IS NOT NULL THEN
        LogPackage.LogChange('UPDATE', 'zamestnanec', 'prijmeni', NULL, p_new_surname, USER);
    END IF;

    -- Update Zamestnanec based on the role
    IF p_role = 'SELF' THEN
        UPDATE zamestnanec z
        SET z.email_zamestnanec = COALESCE(p_new_email, z.email_zamestnanec),
            z.telefoni_cislo = COALESCE(p_new_telefoni_cislo, z.telefoni_cislo),
            z.jmeno = COALESCE(p_new_name, z.jmeno),
            z.prijmeni = COALESCE(p_new_surname, z.prijmeni)
        WHERE z.id_zamestnanec = p_id_zamestnanec;
    ELSIF p_role = 'ADMIN' THEN
        UPDATE zamestnanec z
        SET z.email_zamestnanec = COALESCE(p_new_email, z.email_zamestnanec),
            z.telefoni_cislo = COALESCE(p_new_telefoni_cislo, z.telefoni_cislo),
            z.jmeno = COALESCE(p_new_name, z.jmeno),
            z.prijmeni = COALESCE(p_new_surname, z.prijmeni),
            z.mzda = COALESCE(p_new_mzda, z.mzda),
            z.PRACE_POZICE_ID_POZICE = COALESCE(p_new_id_prace_pozice, z.PRACE_POZICE_ID_POZICE)
        WHERE z.id_zamestnanec = p_id_zamestnanec;
    ELSE
        -- Invalid role, handle accordingly
        raise_application_error(-20223, 'Invalid role specified');
    END IF;

    COMMIT;
END EditZamestnanec;
/

CREATE OR REPLACE TRIGGER HashPasswordTrigger
BEFORE INSERT ON login
FOR EACH ROW
DECLARE
    v_hashed_password VARCHAR2(100);
BEGIN
    -- Hash the password using DBMS_OBFUSCATION_TOOLKIT
    v_hashed_password := DBMS_OBFUSCATION_TOOLKIT.MD5(input => UTL_RAW.CAST_TO_RAW(:NEW.heslo));

    -- Update the NEW record with the hashed password
    :NEW.heslo := v_hashed_password;
END HashPasswordTrigger;
/

CREATE OR REPLACE FUNCTION CheckPassword(
    p_email IN VARCHAR2,
    p_password IN VARCHAR2
) RETURN BOOLEAN
IS
    v_hashed_password VARCHAR2(100);
    v_count NUMBER;
BEGIN
    -- Hash the input password using DBMS_OBFUSCATION_TOOLKIT
    v_hashed_password := DBMS_OBFUSCATION_TOOLKIT.MD5(input => UTL_RAW.CAST_TO_RAW(p_password));

    -- Check if the hashed password matches the one in the database
    SELECT COUNT(*)
    INTO v_count
    FROM login
    WHERE klient_id_klient IS NOT NULL
      AND zamestnanec_id_zamestnanec IS NULL
      AND email = p_email
      AND heslo = v_hashed_password;

    -- Return TRUE if a match is found, FALSE otherwise
    RETURN v_count > 0;
END CheckPassword;
/

CREATE OR REPLACE PROCEDURE OpenNewUcet(
    p_id_klient IN NUMBER,
    p_cislo_uctu IN NUMBER,
    p_nazev IN VARCHAR2,
    p_id_bank IN NUMBER
) AS
    v_id_ucet NUMBER;
    p_role VARCHAR2(30);
BEGIN

    -- Check if the user has the role of 'ZAMESTNANEC'
    IF p_role IN ('ZAMESTNANEC', 'ADMIN') THEN
        -- Get the next ID for ucet
        SELECT MAX(id_ucet) + 1 INTO v_id_ucet FROM ucet;

        -- Insert the new ucet
        INSERT INTO ucet (id_ucet, cislo_uctu, nazev, klient_id_klient, bank_id_bank, status_id_status)
        VALUES (v_id_ucet, p_cislo_uctu, p_nazev, p_id_klient, p_id_bank, 1);

        -- Log the addition
        LogPackage.LogChange('INSERT', 'ucet', 'id_ucet', NULL, v_id_ucet, USER);

        COMMIT;
    ELSE
        raise_application_error(-20223, 'Insufficient privileges');
    END IF;
END OpenNewUcet;
/

CREATE OR REPLACE PROCEDURE OrderNewCard(
    p_ucet_id_ucet IN NUMBER,
    p_jmeno IN VARCHAR2,
    p_prijmeni IN VARCHAR2,
    p_cislo_karty IN NUMBER,
    p_platebni_system IN VARCHAR2,
    p_platnost IN DATE,
    p_typ IN VARCHAR2
) AS
    v_id_karta NUMBER;
BEGIN
    -- Get the next ID for karta
    SELECT MAX(id_karta) + 1 INTO v_id_karta FROM karta;

    -- Insert the new karta
    INSERT INTO karta (id_karta, jmeno, prijmeni, cislo_karty, platebni_system, platnost, typ, ucet_id_ucet)
    VALUES (v_id_karta, p_jmeno, p_prijmeni, p_cislo_karty, p_platebni_system, p_platnost, p_typ, p_ucet_id_ucet);

    -- Log the addition
    LogPackage.LogChange('INSERT', 'karta', 'id_karta', NULL, v_id_karta, USER);

    -- Commit the transaction
    COMMIT;
END OrderNewCard;
/

CREATE OR REPLACE VIEW zamestnanec_hierarchy_view AS
SELECT
    z.id_zamestnanec,
    z.jmeno,
    z.prijmeni,
    z.prace_pozice_id_pozice,
    z.zamestnanec_id_zamestnanec1
FROM
    zamestnanec z;

CREATE OR REPLACE PROCEDURE GetHierarchyInformation(
    p_zamestnanec_id IN NUMBER
) AS
    -- Cursor to fetch klients managed by the employee
    CURSOR klients_cursor IS
        SELECT
            k.id_klient,
            k.jmeno AS klient_jmeno,
            k.prijmeni AS klient_prijmeni
        FROM
            klient k
        WHERE
            k.zame_id_zamestnanec = p_zamestnanec_id;

    -- Cursor to fetch bankers managed by the manager
    CURSOR bankers_cursor IS
        SELECT
            b.id_zamestnanec,
            b.jmeno AS banker_jmeno,
            b.prijmeni AS banker_prijmeni
        FROM
            zamestnanec_hierarchy_view b
        WHERE
            b.zamestnanec_id_zamestnanec1 = '1';

    -- Cursor to fetch managers managed by the top-manager
    CURSOR managers_cursor IS
        SELECT
            m.id_zamestnanec,
            m.jmeno AS manager_jmeno,
            m.prijmeni AS manager_prijmeni
        FROM
            zamestnanec_hierarchy_view m
        WHERE
            m.zamestnanec_id_zamestnanec1 = '2';

    -- Variables to hold cursor records
    klients_rec klients_cursor%ROWTYPE;
    bankers_rec bankers_cursor%ROWTYPE;
    managers_rec managers_cursor%ROWTYPE;

BEGIN
    -- Fetch klients
    FOR klients_rec IN klients_cursor LOOP
        DBMS_OUTPUT.PUT_LINE('Klient: ' || klients_rec.klient_jmeno || ' ' || klients_rec.klient_prijmeni);
    END LOOP;

    -- Fetch bankers
    FOR bankers_rec IN bankers_cursor LOOP
        DBMS_OUTPUT.PUT_LINE('Banker: ' || bankers_rec.banker_jmeno || ' ' || bankers_rec.banker_prijmeni);
    END LOOP;

    -- Fetch managers
    FOR managers_rec IN managers_cursor LOOP
        DBMS_OUTPUT.PUT_LINE('Manager: ' || managers_rec.manager_jmeno || ' ' || managers_rec.manager_prijmeni);
    END LOOP;
END GetHierarchyInformation;
/
