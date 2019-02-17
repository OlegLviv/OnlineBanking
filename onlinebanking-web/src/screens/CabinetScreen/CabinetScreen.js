import React from 'react';
import CabinetLayout from '../../layouts/CabinetLayout/CabinetLayout';
import CreditCardsContainer from '../../containers/CreditCardsContainer/CreditCardsContainer';
import AccountProvider from '../../providers/AccountProvider';
import OrderCreditCardContainer from '../../containers/OrderCreditCardContainer/OrderCreditCardContainer';
import CreditCardContainer from '../../containers/CreditCardContainer/CreditCardContainer';
import CreditCardEditPinContainer from '../../containers/CreditCardEditPinContainer/CreditCardEditPinContainer';
import CreditCardChangeCreditLimitContainer from '../../containers/CreditCardChangeCreditLimitContainer/CreditCardChangeCreditLimitContainer'
import DepositContainer from '../../containers/DepositContainer/DepositContainer';
import MyDepositsContainer from '../../containers/MyDepositsContainer/MyDepositsContainer';

import { Switch, Route } from 'react-router-dom';

// const userRoles = {
//     admin: 'admin',
//     manager: 'manager',
//     user: 'user'
// };

class CabinetScreen extends React.Component {

    render() {
        const { match } = this.props;

        return (
            <CabinetLayout role={match.params.role}>
                <AccountProvider>
                    <Switch>
                        <Route path={`/cab/:role/credit-cards/list`} component={CreditCardsContainer} />
                        <Route path={`/cab/:role/credit-cards/order`} component={OrderCreditCardContainer} />
                        <Route exact path="/cab/:role/credit-card/:id" component={CreditCardContainer} />
                        <Route path="/cab/:role/credit-card/:id/change-pin" component={CreditCardEditPinContainer} />
                        <Route path="/cab/:role/credit-card/:id/change-c-limit" component={CreditCardChangeCreditLimitContainer} />
                        <Route path="/cab/:role/deposit/:currency" component={DepositContainer} />
                        <Route path="/cab/:role/deposits" component={MyDepositsContainer} />
                    </Switch>
                </AccountProvider>
            </CabinetLayout>
        );
    }
}

export default CabinetScreen;