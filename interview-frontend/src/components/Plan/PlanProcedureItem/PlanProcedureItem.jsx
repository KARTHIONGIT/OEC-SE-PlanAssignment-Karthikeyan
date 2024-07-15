import React, { useState, useEffect } from "react";
import ReactSelect from "react-select";

import { addProcedureUsers, cleanUpProcedureUsers } from "../../../api/api";

const PlanProcedureItem = ({ planId, procedure, users, procedureUsers }) => {

  const currentProcedureId = procedure.procedureId;
  const [selectedUsers, setSelectedUsers] = useState(null);
  const [options, setOptions] = useState(users);

  useEffect(() => {
    if (procedureUsers) {
      var filteredProcedures = procedureUsers.filter(
        (x) => x.procedureId === currentProcedureId
      );
      var filteredUsers = filteredProcedures.map((x) => x.userId);
      var procedureMappedUsers = users.filter((x) =>
        filteredUsers.includes(x.value)
      );
      var procedureUnmappedUsers = users.filter(
        (x) => !filteredUsers.includes(x.value)
      );
      setOptions(procedureUnmappedUsers);
      setSelectedUsers(procedureMappedUsers);
    }
    return async() => {
      procedureUsers?.some(x => x.userId === 0) && await cleanUpProcedureUsers();
    }
  }, []);

  const handleAssignUserToProcedure = async (e) => {
    try {
      setSelectedUsers(e);
      if (e?.length === 0) {
        setOptions(users);
        const addResponse = await addProcedureUsers(planId, currentProcedureId, []);
      } else {
        let userIds = e.map(x => x.value);
        const addResponse = await addProcedureUsers(planId, currentProcedureId, userIds);
        if (!addResponse.ok) {
          throw new Error(
            `Error adding users to procedure. error status: ${addResponse.status}`
          );
        }
      }
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className="py-2">
      <div>{procedure.procedureTitle}</div>

      <ReactSelect
        className="mt-2"
        placeholder="Select User to Assign"
        isMulti={true}
        options={options}
        value={selectedUsers}
        onChange={(e) => handleAssignUserToProcedure(e)}
      />
    </div>
  );
};

export default PlanProcedureItem;
