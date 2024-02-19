import React, { useEffect, useState } from "react";
import classes from "./Persons.module.css";
import Modal from "../../views/Modal/Modal";
import ButtonDelete from "../../views/ButtonDelete/ButtonDelete";
import ButtonLink from "../../views/ButtonLink/ButtonLink";
import BigLoader from "../../views/BigLoader/BigLoader";
import HeaderAdd from "../../layout/HeaderAdd/HeaderAdd";
import useFetching from "../../hooks/useFetching";
import PersonApi from "../../api/personApi";
import Input from "../../views/Input/Input";
import Button from "../../views/Button/Button";

const Persons = () => {
  const [isOpenModal, isOpenModalChange] = useState(false);
  const [deletedId, deletedIdChange] = useState(null);
  const [persons, personsChange] = useState([]);
  const [newPersonName, newPersonNameChange] = useState("");
  const [newPersonEmail, newPersonEmailChange] = useState("");
  const [newPersonDate, newPersonDateChange] = useState("");

  const getCallback = async () => {
    const response = await PersonApi.get();
    personsChange(response.data);
    console.log(response.data);
  };

  const deleteCallback = async (id) => {
    await PersonApi.delete(id);
    const newCompetitions = persons.filter(
      (competition) => competition.id !== id
    );
    personsChange([...newCompetitions]);
    deletedIdChange(null);
  };

  const addCallback = async () => {
    const params = {
      name: newPersonName,
      dateOfBirth: newPersonDate,
      email: newPersonEmail,
    };
    await PersonApi.add(params);
    isOpenModalChange(false);
    newPersonNameChange("");
    newPersonDateChange("");
    newPersonEmailChange("");
  };

  const [fetchGet, isLoadingfetchGet] = useFetching(getCallback);
  const [fetchDelete, isLoadingDelete] = useFetching(deleteCallback);
  const [fetchAdd, isLoadingAdd] = useFetching(addCallback);

  const del = (id) => {
    if (isLoadingDelete) return;
    deletedIdChange(id);
    fetchDelete(id);
  };

  const openModal = () => {
    isOpenModalChange(true);
  };

  useEffect(() => {
    fetchGet();
  }, []);

  const Head = () => (
    <HeaderAdd
      text="Люди"
      className={classes.header}
      onClick={openModal}
      title="Добавить человека"
    />
  );

  if (isLoadingfetchGet)
    return (
      <div>
        <Head />
        <BigLoader />
      </div>
    );

  return (
    <div>
      <Head />
      <table>
        <thead>
          <tr>
            <th>Имя</th>
            <th>Email</th>
            <th>Дата рождения</th>
            <th>Открыть</th>
            <th>Удалить</th>
          </tr>
        </thead>
        <tbody>
          {persons.map((person, index) => (
            <tr key={index}>
              <td className={classes.tableName}>{person.name}</td>
              <td className={classes.tableDate}>{person.email}</td>
              <td className={classes.tableSport}>{person.dateOfBirth}</td>
              <td className={classes.tableButton}>
                <ButtonLink text="Открыть" to={`/persons/${person.id}`} />
              </td>
              <td className={classes.tableButton}>
                <ButtonDelete
                  text="Удалить"
                  onClick={() => del(person.id)}
                  isLoading={isLoadingDelete && person.id === deletedId}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Modal active={isOpenModal} setActive={isOpenModalChange}>
        <div className={classes.modalBody}>
          <div className={classes.modalLogo}>Добавить человека</div>
          <Input
            value={newPersonName}
            setValue={newPersonNameChange}
            placeholder="Имя"
            className={classes.inputName}
          />
          <Input
            value={newPersonEmail}
            setValue={newPersonEmailChange}
            placeholder="Email"
            className={classes.inputEmail}
          />
          <input
            type="datetime-local"
            value={newPersonDate}
            onChange={(e) => newPersonDateChange(e.target.value)}
            className={classes.inputDate}
          />
          <Button
            text={"Добавить"}
            className={classes.addButton}
            isLoading={isLoadingAdd}
            onClick={fetchAdd}
          />
        </div>
      </Modal>
    </div>
  );
};

export default Persons;
