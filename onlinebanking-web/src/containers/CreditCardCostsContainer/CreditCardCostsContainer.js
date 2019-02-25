import React from 'react';
import BaseContainer from '../BaseContainer';
import './CreditCardCostsContainer.css';

import { connect } from 'react-redux'
import { fetchCostsLog } from '../../actions/crediCards';
import { Pagination, List } from 'antd';
import { toDateAndTime } from '../../utils/timeUtils';

const itemPerPage = 10;

const getIdFromPros = ({ match }) => match.params.id;

export class CreditCardCostsContainer extends BaseContainer {
  state = {
    page: 1
  }

  componentWillMount() {
    this.props.fetchCostsLog(getIdFromPros(this.props), itemPerPage, this.state.page);
  }

  componentWillUpdate(_, nextState) {
    if (this.state.page !== nextState.page)
      this.props.fetchCostsLog(getIdFromPros(this.props), itemPerPage, nextState.page);
  }

  onPaginationChange = page => this.setState({ page });

  render() {
    const { creditCardCosts } = this.props;

    return (
      <>
        <List
          dataSource={creditCardCosts.costsLog ? creditCardCosts.costsLog.data : []}
          loading={creditCardCosts.loading}
          renderItem={item => (
            <List.Item>
              <div className="costs-item">
                <div className="costs-item-sub">
                  <div>{`Amount: ${item.amount}`}</div>
                  <small>{`Date: ${toDateAndTime(item.date)}`}</small>
                </div>
                <div className="costs-item-sub">
                  <div>{`to ${item.destinationUser.name} ${item.destinationUser.name}`}</div>
                </div>
              </div>
            </List.Item>
          )} />
        {
          !creditCardCosts.loading && creditCardCosts && creditCardCosts.costsLog && <Pagination
            onChange={this.onPaginationChange}
            pageSize={itemPerPage}
            current={this.state.page}
            total={creditCardCosts.costsLog.total} />
        }
      </>
    )
  }
}

export default connect(
  state => ({
    creditCardCosts: state.creditCards
  }),
  dispatch => ({
    fetchCostsLog: (id, itemPerPage, page) => dispatch(fetchCostsLog(id, itemPerPage, page))
  })
)(CreditCardCostsContainer);
