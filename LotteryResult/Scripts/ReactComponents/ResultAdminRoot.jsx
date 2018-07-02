class ResultAdminRoot extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            resultEntries: [],
            rewardType: {
                id: NaN,
                name: "-",
                instance: NaN,
                format: "-",
                reward_amount: "-"
            },
            round: {
                id: NaN,
                round: "-",
                date: "-"
            }
        };

        this.addResultEntry = this.addResultEntry.bind(this);
    }

    addResultEntry(resultOrder, result) {
        this.setState((prevState, props) => {
            prevState.resultEntries.push({
                resultOrder: resultOrder,
                result: result
            });
            return { resultEntries: prevState.resultEntries };
        });
    }

    componentDidMount() {
        $.ajax({
            url: "GetRoundAndRewardType",
            method: "GET"
        }).success((data, textStatus, jqXHR) => {
            if (data.status == 'ok') {
                this.setState({
                    round: data.payload.round,
                    rewardType: data.payload.rewardType
                });
            }
        });
    }

    render() {
        return (
            <div className="row">
                <div className="col-md-3">
                    <RoundBox round={this.state.round} />
                    <RewardTypeBox rewardType={this.state.rewardType} />
                </div>
                <div className="col-md-9">
                    <ResultAdminValidate round={this.state.round} rewardType={this.state.rewardType} addResultEntry={this.addResultEntry} />
                    <ResultCollection resultEntries={this.state.resultEntries} />
                </div>
            </div>
        );
    }
}

ReactDOM.render(
    <ResultAdminRoot />,
    document.getElementById('ResultAdminRoot')
);