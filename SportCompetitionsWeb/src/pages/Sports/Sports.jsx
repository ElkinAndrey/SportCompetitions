import React, { useEffect, useState } from "react";
import classes from "./Sports.module.css";
import Modal from "../../views/Modal/Modal";
import ButtonDelete from "../../views/ButtonDelete/ButtonDelete";
import BigLoader from "../../views/BigLoader/BigLoader";
import HeaderAdd from "../../layout/HeaderAdd/HeaderAdd";
import useFetching from "../../hooks/useFetching";
import SportApi from "../../api/sportApi";
import Input from "../../views/Input/Input";
import Button from "../../views/Button/Button";

const Sports = () => {
  const [isAddOpenModal, isAddOpenModalChange] = useState(false);
  const [isChangeOpenModal, isChangeOpenModalChange] = useState(false);
  const [deletedId, deletedIdChange] = useState(null);
  const [sports, sportsChange] = useState([]);
  const [newName, newNameChange] = useState("");
  const [newDescription, newDescriptionChange] = useState("");
  const [changeId, changeIdChange] = useState("");
  const [changeName, changeNameChange] = useState("");
  const [changeDescription, changeDescriptionChange] = useState("");

  const getCallback = async () => {
    const response = await SportApi.get();
    sportsChange(response.data);
  };

  const deleteCallback = async (id) => {
    await SportApi.delete(id);
    const newSports = sports.filter((competition) => competition.id !== id);
    sportsChange([...newSports]);
    deletedIdChange(null);
  };

  const addCallback = async () => {
    const p = {
      name: newName,
      description: newDescription,
    };
    await SportApi.add(p);
    isAddOpenModalChange(false);
    newDescriptionChange("");
    newNameChange("");
    fetchGet();
  };

  const changeCallback = async () => {
    const p = {
      name: changeName,
      description: changeDescription,
    };
    await SportApi.change(changeId, p);
    isChangeOpenModalChange(false);
    const index = sports.findIndex((sport) => sport.id === changeId);
    sports[index].name = changeName;
    sports[index].description = changeDescription;
    sportsChange([...sports]);
  };

  const [fetchGet, isLoadingfetchGet] = useFetching(getCallback);
  const [fetchDelete, isLoadingDelete] = useFetching(deleteCallback);
  const [fetchAdd, isLoadingAdd] = useFetching(addCallback);
  const [fetchChange, isLoadingChange] = useFetching(changeCallback);

  const del = (id) => {
    if (isLoadingDelete) return;
    deletedIdChange(id);
    fetchDelete(id);
  };

  const add = () => {
    isAddOpenModalChange(true);
  };

  const change = (competition) => {
    changeIdChange(competition.id);
    changeNameChange(competition.name);
    changeDescriptionChange(competition.description);
    isChangeOpenModalChange(true);
  };

  useEffect(() => {
    fetchGet();
  }, []);

  const Head = () => (
    <HeaderAdd
      text="Виды спорта"
      className={classes.header}
      onClick={add}
      title="Добавить вид спорта"
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
            <th>Название</th>
            <th>Описание</th>
            <th>Изменить</th>
            <th>Удалить</th>
          </tr>
        </thead>
        <tbody>
          {sports.map((competition, index) => (
            <tr key={index}>
              <td className={classes.tableName}>{competition.name}</td>
              <td className={classes.tableDate}>{competition.description}</td>
              <td className={classes.tableButton}>
                <Button text={"Изменить"} onClick={() => change(competition)} />
              </td>
              <td className={classes.tableButton}>
                <ButtonDelete
                  text="Удалить"
                  onClick={() => del(competition.id)}
                  isLoading={isLoadingDelete && competition.id === deletedId}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Modal active={isAddOpenModal} setActive={isAddOpenModalChange}>
        <div className={classes.modalBody}>
          <div className={classes.modalLogo}>Добавить вид спорта</div>
          <Input
            value={newName}
            setValue={newNameChange}
            placeholder="Название"
            className={classes.inputName}
          />
          <Input
            value={newDescription}
            setValue={newDescriptionChange}
            placeholder="Описание"
            className={classes.inputEmail}
          />
          <Button
            text={"Добавить"}
            className={classes.addButton}
            isLoading={isLoadingAdd}
            onClick={fetchAdd}
          />
        </div>
      </Modal>
      <Modal active={isChangeOpenModal} setActive={isChangeOpenModalChange}>
        <div className={classes.modalBody}>
          <div className={classes.modalLogo}>Изменить вид спорта</div>
          <Input
            value={changeName}
            setValue={changeNameChange}
            placeholder="Название"
            className={classes.inputName}
          />
          <Input
            value={changeDescription}
            setValue={changeDescriptionChange}
            placeholder="Описание"
            className={classes.inputEmail}
          />
          <Button
            text={"Сохранить"}
            className={classes.addButton}
            isLoading={isLoadingChange}
            onClick={fetchChange}
          />
        </div>
      </Modal>
    </div>
  );
};

export default Sports;
