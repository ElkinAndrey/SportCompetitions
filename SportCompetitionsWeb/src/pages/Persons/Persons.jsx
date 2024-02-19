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
  const [newName, newNameChange] = useState("");
  const [newEmail, newEmailChange] = useState("");
  const [newDate, newDateChange] = useState("");

  const getCallback = async () => {
    const response = await PersonApi.get();
    personsChange(response.data);
  };

  const deleteCallback = async (id) => {
    await PersonApi.delete(id);
    const newPersons = persons.filter((competition) => competition.id !== id);
    personsChange([...newPersons]);
    deletedIdChange(null);
  };

  const addCallback = async () => {
    const p = {
      name: newName,
      dateOfBirth: newDate,
      email: newEmail,
    };
    await PersonApi.add(p);
    isOpenModalChange(false);
    newNameChange("");
    newDateChange("");
    newEmailChange("");
    fetchGet();
  };

  const [fetchGet, isLoadingfetchGet] = useFetching(getCallback);
  const [fetchDelete, isLoadingDelete] = useFetching(deleteCallback);
  const [fetchAdd, isLoadingAdd] = useFetching(addCallback);

  const del = (id) => {
    if (isLoadingDelete) return;
    deletedIdChange(id);
    fetchDelete(id);
  };

  const add = () => {
    isOpenModalChange(true);
  };

  useEffect(() => {
    fetchGet();
  }, []);

  const Head = () => (
    <HeaderAdd
      text="Люди"
      className={classes.header}
      onClick={add}
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
            value={newName}
            setValue={newNameChange}
            placeholder="Имя"
            className={classes.inputName}
          />
          <Input
            value={newEmail}
            setValue={newEmailChange}
            placeholder="Email"
            className={classes.inputEmail}
          />
          <input
            type="date"
            value={newDate}
            onChange={(e) => newDateChange(e.target.value)}
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
