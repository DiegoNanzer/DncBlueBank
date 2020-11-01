import { UncontrolledAlert } from 'reactstrap';
import { useContext } from 'react';
import React from 'react';
import { AlertaDispatchContext, AlertStateContext } from '../context/alertContext';


export const MyAlert: React.FC = () => {
    const alertCtx = useContext(AlertStateContext);
    const alertaDispatch = useContext(AlertaDispatchContext);

    const onDismiss = () => alertaDispatch({ typeAction: "HIDDEN" });

    return (
        <div>
            <UncontrolledAlert color={alertCtx.type == "SUCCESS" ? "success" : "danger"}
                style={{ position: 'fixed', top: 65, right: 10, }} isOpen={alertCtx.visible} toggle={onDismiss} fade={false}>
                {alertCtx.message}
            </UncontrolledAlert>
        </div>
    );
}

